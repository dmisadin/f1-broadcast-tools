import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { PlayerService } from '../../services/player.service';
import { LookupDto } from '../../models/common';

@Component({
    selector: 'player-search',
    standalone: false,
    templateUrl: './player-search.component.html',
})
export class PlayerSearchComponent implements OnInit {
    players$: Observable<LookupDto[]>;
    searchSubject = new Subject<string>();
    @Input() selectedId?: number;
    @Input() selectedLabel?: string;
    @Output() selectedIdChange = new EventEmitter<number>();
    @Output() onClear = new EventEmitter();

    constructor(private playerService: PlayerService) { }

    ngOnInit(): void {
        this.players$ = this.searchSubject.pipe(
            startWith(this.selectedLabel ?? ""),
            debounceTime(300),
            distinctUntilChanged(),
            switchMap(query => this.playerService.searchPlayers(query))
        );
    }

    onPlayerChange(selectedId: number) {
        this.selectedIdChange.emit(selectedId);
    }
}
