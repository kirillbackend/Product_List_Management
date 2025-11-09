import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductDto } from '../models/product-dto';
import { ProductShortDto } from '../models/product-short-dto';
import { FilterDto } from '../models/filter-dto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:7215/api/product';

  constructor(private http: HttpClient) { }

  getProduct(id: string): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${this.apiUrl}/${id}`);
  }

  getProducts(filter: FilterDto): Observable<ProductShortDto[]> {
    let params = new HttpParams();
    
    if (filter.name) params = params.set('name', filter.name);
    if (filter.minPrice) params = params.set('minPrice', filter.minPrice.toString());
    if (filter.maxPrice) params = params.set('maxPrice', filter.maxPrice.toString());
    if (filter.category) params = params.set('category', filter.category);
    if (filter.page) params = params.set('page', filter.page.toString());
    if (filter.pageSize) params = params.set('pageSize', filter.pageSize.toString());
    if (filter.sortBy) params = params.set('sortBy', filter.sortBy);
    if (filter.sortDescending) params = params.set('sortDescending', filter.sortDescending.toString());

    return this.http.get<ProductShortDto[]>(this.apiUrl, { params });
  }

  createProduct(product: ProductDto): Observable<void> {
    return this.http.post<void>(this.apiUrl, product);
  }

  updateProduct(product: ProductDto): Observable<ProductDto> {
    return this.http.put<ProductDto>(this.apiUrl, product);
  }
}