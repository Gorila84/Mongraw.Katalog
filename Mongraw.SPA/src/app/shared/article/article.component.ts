import { Component, Input } from '@angular/core';
import { CircleImageComponent } from "../circle-image/circle-image.component";
import { Article } from '../interfaces/article';
import { CommonModule } from '@angular/common';
import { StorkedButtonComponent } from "../controls/storked-button/storked-button.component";


@Component({
  selector: 'app-article',
  imports: [CircleImageComponent, CommonModule, StorkedButtonComponent],
  templateUrl: './article.component.html',
  styleUrl: './article.component.scss'
})
export class ArticleComponent {
@Input() article: Article ={} as Article;
@Input() index: number = 0;



}
