import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { AuthService } from './auth.service';
describe('AuthService', () => {
  let service: AuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],  // ← Добавлен HttpClientTestingModule
      providers: [AuthService]
    });
    service = TestBed.inject(AuthService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should set and get token', () => {
    service.setToken('test-token');
    expect(service.getToken()).toBe('test-token');
  });

  it('should remove token', () => {
    service.setToken('test-token');
    service.removeToken();
    expect(service.getToken()).toBeNull();
  });

  it('should check authentication', () => {
    service.setToken('test-token');
    expect(service.isAuthenticated()).toBeTrue();
    
    service.removeToken();
    expect(service.isAuthenticated()).toBeFalse();
  });
});