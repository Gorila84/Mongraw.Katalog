import { Component, EventEmitter, Output, Input, OnInit, HostBinding } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';

@Component({
  standalone: true,
  imports: [MatButtonModule],
  selector: 'app-storked-button',
  templateUrl: './storked-button.component.html',
  styleUrls: ['./storked-button.component.scss']
})
export class StorkedButtonComponent implements OnInit {
  @Output() clickEvent = new EventEmitter<void>();
  @Input() color?: string;
  defaultColor = '#b28235';

  @HostBinding('style.--btn-color') get btnColor() {
    return this.color || this.defaultColor;
  }

  ngOnInit(): void {}

  handleEvent(event: Event) {
    this.clickEvent.emit();
  }
}
