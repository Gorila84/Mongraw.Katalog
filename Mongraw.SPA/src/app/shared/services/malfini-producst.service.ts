import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MalfiniProductListDto } from '../interfaces/productListDto';
import { environment } from '../../../enviroments/enviroment';
import { ProductParams } from '../interfaces/productParams';
import { PagedResult } from '../interfaces/pageResult';

@Injectable({
  providedIn: 'root'
})
export class MalfiniProducstService {

  baseUrl = environment.apiUrl + 'malfiniProduct/';
  constructor(private http: HttpClient) { }


  getMalfiniProducts(params: ProductParams): Observable<PagedResult<MalfiniProductListDto>> {
    let httpParams = new HttpParams()
      .set('pageNumber', params.pageNumber)
      .set('pageSize', params.pageSize);

    if (params.name) {
      httpParams = httpParams.set('name', params.name);
    }
    if (params.color) {
      httpParams = httpParams.set('color', params.color);
    }
    if (params.categoryCode) {
      httpParams = httpParams.set('categoryCode', params.categoryCode);
    }
    if (params.genderCode) {
      httpParams = httpParams.set('genderCode', params.genderCode);
    }

    return this.http.get<PagedResult<MalfiniProductListDto>>(this.baseUrl + 'getMalfiniProducts', { params: httpParams });
  }
}
