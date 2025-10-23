import { Component, Input, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
@Component({
  selector: 'app-card',
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatButtonToggleModule],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss',
})
export class CardComponent implements OnInit {
  @Input() title!: string;
  @Input() subtitle!: string;
  @Input() altText!: string;
  @Input() links!: { key: string; url: string }[];
  @Input() imageUrl!: string;
  @Input() icon!: string;

  constructor() {
  console.log('CardComponent created');
}
ngOnInit() {
  console.log('CardComponent initialized');
}

}
