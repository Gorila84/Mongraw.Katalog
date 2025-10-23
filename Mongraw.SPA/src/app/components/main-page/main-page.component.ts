import { AfterViewInit, Component, ElementRef, inject, OnInit, QueryList, ViewChildren } from '@angular/core';
import { HeaderBanerComponent } from "../../core/header-baner/header-baner.component";
import { ArticleComponent } from "../../shared/article/article.component";
import { Article } from '../../shared/interfaces/article';
import { CommonModule } from '@angular/common';
import { StorkedButtonComponent } from "../../shared/controls/storked-button/storked-button.component";
import { MatButton } from "@angular/material/button";
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-main-page',
  imports: [HeaderBanerComponent, ArticleComponent, CommonModule, StorkedButtonComponent, MatButton],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent implements OnInit, AfterViewInit  {
  
  @ViewChildren('observeElement') observeElements!: QueryList<ElementRef>;

  isVisible: boolean[] = [];
  router = inject(Router);
  articles: Article[] = []
  ngOnInit(): void {
    this.articles = [
      {
        title: 'Druk na tekstyliach',
        content: 'Oferujemy wysokiej jakości druk na tekstyliach z wykorzystaniem nowoczesnych technologii DTF (Direct to Film) oraz sublimacji. Dzięki tym metodom zapewniamy trwałe, nasycone kolory i precyzyjne odwzorowanie detali na różnych materiałach. Realizujemy zarówno małe nakłady, jak i większe zamówienia – idealne dla firm, marek odzieżowych czy klientów indywidualnych.',
        imageUrl: 'assets/images/tekstylia.jpg',
        url: ''
      },
      {
        title: 'Torby reklamowe',
        content: 'Wykonujemy precyzyjne cięcie oraz grawerowanie laserowe na różnych materiałach, takich jak drewno, sklejka, akryl, laminaty grawerskie, szkło, skóra i wiele innych. To idealne rozwiązanie do tworzenia personalizowanych produktów, elementów dekoracyjnych, tabliczek czy gadżetów reklamowych. Gwarantujemy wysoką jakość i dokładność wykonania.',
        imageUrl: 'assets/images/torby.jpg',
        url: '/articles/second-article'
      },
      // {
      //   title: 'Sublimacja na ceramice',
      //   content: 'Oferujemy trwałe i estetyczne nadruki metodą sublimacji na ceramice, m.in. na kubkach, talerzach, kafelkach i innych produktach. Nadruki charakteryzują się żywymi kolorami i wysoką odpornością na ścieranie oraz mycie. To doskonały sposób na tworzenie personalizowanych upominków, gadżetów reklamowych i elementów dekoracyjnych.',
      //   imageUrl: 'assets/images/kubeczek.jpg',
      //   url: '/articles/third-article'
      // },
      {
        title: 'Gadżety reklamowe',
        content: 'Wykonujemy szeroki wybór gadżetów reklamowych z możliwością personalizacji – idealnych na targi, eventy firmowe czy jako upominki dla klientów. Oferujemy m.in. kubki, breloki, długopisy, magnesy, torby i wiele innych produktów z nadrukiem. Zapewniamy wysoką jakość wykonania oraz indywidualne podejście do każdego projektu.',
        imageUrl: 'assets/images/gadzety.jpg',
        url: '/articles/fourth-article'
      }
    ];
    this.isVisible = new Array(this.articles.length).fill(false);
  }





 ngAfterViewInit(): void {
  const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
      const index = this.observeElements.toArray().findIndex(el => el.nativeElement === entry.target);

      if (entry.isIntersecting && index !== -1) {
        this.isVisible[index] = true;
        observer.unobserve(entry.target); 
      }
    });
  }, {
    threshold: 0.3
  });

  this.observeElements.changes.subscribe((elements: QueryList<ElementRef>) => {
    elements.forEach(el => observer.observe(el.nativeElement));
  });

  this.observeElements.forEach(el => observer.observe(el.nativeElement));
}

moveToContatct(){
  this.router.navigate(['/kontakt']);
}

}
