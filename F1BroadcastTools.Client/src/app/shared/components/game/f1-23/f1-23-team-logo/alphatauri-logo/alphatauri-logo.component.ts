import { Component } from '@angular/core';
import { TeamLogoBase } from '../../../shared/team-logo-base';

@Component({
  selector: 'alphatauri-logo',
  imports: [],
  templateUrl: './alphatauri-logo.component.html',
  styleUrl: './alphatauri-logo.component.css'
})
export class AlphatauriLogoComponent extends TeamLogoBase {
    ngOnInit() {
        console.log(this.color())
    }
}
