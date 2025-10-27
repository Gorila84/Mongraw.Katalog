import { Component, inject, OnInit } from '@angular/core';
import { MalfiniProducstService } from '../../shared/services/malfini-producst.service';
import { MalfiniProductListDto } from '../../shared/interfaces/productListDto';
import { ProductCardComponent } from "../../shared/controls/product-card/product-card.component";
import { ProductParams } from '../../shared/interfaces/productParams';
import { PagedResult } from '../../shared/interfaces/pageResult';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';
@Component({
  selector: 'app-textil-list',
  imports: [ProductCardComponent, MatPaginatorModule],
  templateUrl: './textil-list.component.html',
  styleUrl: './textil-list.component.scss'
})
export class TextilListComponent implements OnInit {

  malfiniProductService = inject(MalfiniProducstService);
  products: MalfiniProductListDto[] = [];
  productParams: ProductParams = {} as ProductParams;
  totalCount = 0;
  
  pageEvent!: PageEvent;

  ngOnInit(): void {
    this.productParams.pageNumber = 1;
    this.productParams.pageSize = 10; 
    this.getProducts();
  }

  getProducts(){
    this.malfiniProductService.getMalfiniProducts(this.productParams).subscribe({
      next: (response: PagedResult<MalfiniProductListDto>) => {
        this.products = response.items;
        this.totalCount = response.totalCount;
      },
      error: (err) => console.error('Błąd pobierania produktów:', err)
    });
  }

    handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.productParams.pageNumber = e.pageIndex + 1;
    this.productParams.pageSize = e.pageSize;
    this.getProducts();
  }
}
