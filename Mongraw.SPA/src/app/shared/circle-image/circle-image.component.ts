import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-circle-image',
  imports: [CommonModule],
  templateUrl: './circle-image.component.html',
  styleUrl: './circle-image.component.scss',
})
export class CircleImageComponent  {
  @Input() imageUrl: string = '';
  @Input() altText: string = '';
 
}
