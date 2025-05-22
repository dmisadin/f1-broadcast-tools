import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { PlayerService } from '../../services/player.service';
import { LookupDto } from '../../models/common';

@Component({
    selector: 'player-search',
    standalone: false,
    templateUrl: './player-search.component.html',
})
export class PlayerSearchComponent implements OnInit {
    players$ = new BehaviorSubject<LookupDto[]>([]);
    searchSubject = new Subject<string>();
    selectedPlayer?: LookupDto;
    allPlayers: LookupDto[] = [];
    @Input() selectedId?: number;
    @Input() selectedLabel?: string;
    @Output() selectedIdChange = new EventEmitter<number>();
    @Output() onClear = new EventEmitter();

    constructor(private playerService: PlayerService) { }

    ngOnInit(): void {
        const initialSelected: LookupDto | undefined = this.selectedId && this.selectedLabel
            ? { id: this.selectedId, label: this.selectedLabel }
            : undefined;

        if (initialSelected) {
            this.players$.next([initialSelected]); // preload it into the list
        }

        this.searchSubject.pipe(
            debounceTime(300),
            distinctUntilChanged(),
            switchMap(query => this.playerService.searchPlayers(query)),
            tap(searchResults => {
                const merged = [...new Map([
                    ...(initialSelected ? [initialSelected] : []),
                    ...searchResults
                ].map(p => [p.id, p])).values()];

                this.players$.next(merged);
            })
        ).subscribe();
    }

    onPlayerChange(selectedId: number) {
        this.selectedIdChange.emit(selectedId);
    }
}
