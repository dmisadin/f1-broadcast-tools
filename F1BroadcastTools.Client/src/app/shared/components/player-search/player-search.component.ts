import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
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
    selectedId: number;
    @Output() onSelected = new EventEmitter<number>();

    constructor(private playerService: PlayerService) { }

    ngOnInit(): void {
        this.players$ = this.searchSubject.pipe(
            debounceTime(300),
            distinctUntilChanged(),
            switchMap(query => this.playerService.searchPlayers(query))
        );
    }

    onPlayerSelected(selectedId: number) {
        console.log(selectedId)
        this.onSelected.emit(selectedId);
    }
}
