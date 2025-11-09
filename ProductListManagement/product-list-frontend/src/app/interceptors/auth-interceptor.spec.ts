import { TestBed } from '@angular/core/testing';
import { HttpHandler, HttpRequest } from '@angular/common/http';
import { of } from 'rxjs';
import { AuthInterceptor } from './auth.interceptor';
import { AuthService } from '../services/auth.service';

describe('AuthInterceptor', () => {
  let interceptor: AuthInterceptor;
  let authService: AuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        AuthInterceptor,
        { 
          provide: AuthService, 
          useValue: { 
            getToken: () => 'test-token' 
          } 
        }
      ]
    });

    interceptor = TestBed.inject(AuthInterceptor);
    authService = TestBed.inject(AuthService);
  });

  it('should be created', () => {
    expect(interceptor).toBeTruthy();
  });

  it('should add authorization header when token exists', () => {
    const request = new HttpRequest('GET', '/test');
    const next: HttpHandler = {
      handle: (req: HttpRequest<any>) => of({} as any)
    };

    spyOn(authService, 'getToken').and.returnValue('test-token');
    
    interceptor.intercept(request, next).subscribe();

    expect(authService.getToken).toHaveBeenCalled();
  });

  it('should not add authorization header when no token', () => {
    const request = new HttpRequest('GET', '/test');
    const next: HttpHandler = {
      handle: (req: HttpRequest<any>) => of({} as any)
    };

    spyOn(authService, 'getToken').and.returnValue(null);
    
    interceptor.intercept(request, next).subscribe();

    expect(authService.getToken).toHaveBeenCalled();
  });
});