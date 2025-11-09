export interface FilterDto {
  name?: string;
  minPrice?: number;
  maxPrice?: number;
  category?: string;
  page?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
}