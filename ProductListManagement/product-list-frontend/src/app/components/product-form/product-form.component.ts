import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { ProductDto } from '../../models/product-dto';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {
  productForm: FormGroup;
  isEdit = false;
  productId?: string;
  loading = false;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    private productService: ProductService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.productForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],  // ← name вместо productName
      description: ['', [Validators.required, Validators.maxLength(500)]],
      price: [0, [Validators.required, Validators.min(0.01)]]
    });
  }

  ngOnInit(): void {
    this.productId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEdit = !!this.productId;

    if (this.isEdit && this.productId) {
      this.loadProduct(this.productId);
    }
  }

  loadProduct(id: string): void {
    this.loading = true;
    this.productService.getProduct(id).subscribe({
      next: (product) => {
        this.productForm.patchValue({
          name: product.name,  // ← name
          description: product.description,
          price: product.price
        });
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load product';
        this.loading = false;
        console.error('Error loading product:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.productForm.invalid) {
      // Покажем ошибки валидации
      this.productForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    this.error = '';

    const productData: ProductDto = {
      name: this.productForm.value.name,  // ← name
      description: this.productForm.value.description,
      price: this.productForm.value.price
    };

    if (this.isEdit && this.productId) {
      productData.id = this.productId;
      this.updateProduct(productData);
    } else {
      this.createProduct(productData);
    }
  }

  createProduct(product: ProductDto): void {
    this.productService.createProduct(product).subscribe({
      next: () => {
        this.router.navigate(['/products']);
      },
      error: (error) => {
        this.handleError(error);
      }
    });
  }

  updateProduct(product: ProductDto): void {
    this.productService.updateProduct(product).subscribe({
      next: () => {
        this.router.navigate(['/products']);
      },
      error: (error) => {
        this.handleError(error);
      }
    });
  }

  private handleError(error: any): void {
    if (error.error?.errors) {
      // Обработка ошибок валидации с бэкенда
      const validationErrors = error.error.errors;
      this.error = Object.values(validationErrors).flat().join(', ');
    } else {
      this.error = error.error?.message || 'Operation failed';
    }
    this.loading = false;
  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}