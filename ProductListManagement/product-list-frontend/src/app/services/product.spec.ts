import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { ProductService } from './product.service'; 
import { ProductDto } from '../models/product-dto';
import { ProductShortDto } from '../models/product-short-dto';
import { FilterDto } from '../models/filter-dto';

describe('ProductService', () => {
  let service: ProductService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],  // ← Добавлен HttpClientTestingModule
      providers: [ProductService]
    });
    service = TestBed.inject(ProductService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Проверяем, что нет незавершенных запросов
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get product by id', () => {
    const mockProduct: ProductDto = {
      id: '123',
      name: 'Test Product',
      description: 'Test Description',
      price: 100
    };

    service.getProduct('123').subscribe(product => {
      expect(product).toEqual(mockProduct);
    });

    const req = httpMock.expectOne('/api/product/123');
    expect(req.request.method).toBe('GET');
    req.flush(mockProduct);
  });

  it('should get products with filter', () => {
    const mockProducts: ProductShortDto[] = [
      { id: '1', name: 'Product 1', price: 100 },
      { id: '2', name: 'Product 2', price: 200 }
    ];

    const filter: FilterDto = { name: 'test' };

    service.getProducts(filter).subscribe(products => {
      expect(products).toEqual(mockProducts);
    });

    const req = httpMock.expectOne('/api/product?name=test');
    expect(req.request.method).toBe('GET');
    req.flush(mockProducts);
  });

  it('should create product', () => {
    const newProduct: ProductDto = {
      name: 'New Product',
      description: 'New Description',
      price: 150
    };

    service.createProduct(newProduct).subscribe();

    const req = httpMock.expectOne('/api/product');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newProduct);
    req.flush(null);
  });

  it('should update product', () => {
    const updatedProduct: ProductDto = {
      id: '123',
      name: 'Updated Product',
      description: 'Updated Description',
      price: 200
    };

    service.updateProduct(updatedProduct).subscribe(product => {
      expect(product).toEqual(updatedProduct);
    });

    const req = httpMock.expectOne('/api/product');
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updatedProduct);
    req.flush(updatedProduct);
  });
});