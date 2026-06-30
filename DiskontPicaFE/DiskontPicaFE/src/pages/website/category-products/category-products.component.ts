import { CommonModule, DecimalPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Product } from '../../../models/product';
import { ProductService } from '../../../services/product.service';
import { CartService } from '../../../services/cart.service';
import { MatButtonModule } from '@angular/material/button';
import { Category } from '../../../models/category';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-category-products',
  standalone: true,
  imports: [CommonModule, DecimalPipe, MatButtonModule],
  templateUrl: './category-products.component.html',
  styleUrl: './category-products.component.css'
})
export class CategoryProductsComponent implements OnInit {

  allProducts: Product[] = [];
  filteredProducts: Product[] = [];
  allCategories: Category[] = [];
  selectedCategoryId: number | null = null;
  searchTerm: string = '';
  currentSort: string = 'default';

  constructor(
    private productService: ProductService, 
    private cartService: CartService,
    private categoryService: CategoryService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.productService.GetAllProducts().subscribe(data => {
      this.allProducts = data;
      this.filterProducts();
    });
    
    this.categoryService.GetAllCategories().subscribe(data => {
      this.allCategories = data;
    });
  }

  applyFilter(event: any) {
    this.searchTerm = (event.target.value || '').trim().toLowerCase();
    this.filterProducts();
  }

  selectCategory(categoryId: number | null) {
    this.selectedCategoryId = categoryId;
    this.filterProducts();
  }

  changeSort(event: any) {
    this.currentSort = event.target.value;
    this.filterProducts();
  }

  filterProducts() {
    let allowedCategoryIds: number[] | null = null;
    
    if (this.selectedCategoryId !== null) {
      allowedCategoryIds = [this.selectedCategoryId];
      const subCategoryIds = this.allCategories
        .filter(c => c.superCategoryId === this.selectedCategoryId)
        .map(c => c.categoryId);
      allowedCategoryIds.push(...subCategoryIds);
    }

    let result = this.allProducts.filter(p => {
      const matchCategory = allowedCategoryIds === null || allowedCategoryIds.includes(p.categoryId);
      const matchSearch = p.name.toLowerCase().includes(this.searchTerm) || p.description.toLowerCase().includes(this.searchTerm);
      return matchCategory && matchSearch;
    });

    if (this.currentSort === 'name-asc') {
      result.sort((a, b) => a.name.localeCompare(b.name));
    } else if (this.currentSort === 'name-desc') {
      result.sort((a, b) => b.name.localeCompare(a.name));
    } else if (this.currentSort === 'price-asc') {
      result.sort((a, b) => a.price - b.price);
    } else if (this.currentSort === 'price-desc') {
      result.sort((a, b) => b.price - a.price);
    }

    this.filteredProducts = result;
  }

  addToCart(product: Product) {
    this.cartService.addToCart(product);
  }

  /** Returns a local asset path for a product image, or null to use the emoji fallback */
  getProductImage(name: string): string | null {
    const n = name.toLowerCase();
    if (n.includes('vivia water')) return '/assets/Vivia Water.jpg';
    if (n.includes('prolom water')) return '/assets/Prolom Water.jpg';
    if (n.includes('coca-cola')) return '/assets/Coca-Cola.jpg';
    if (n.includes('fanta')) return '/assets/fanta.jpg';
    if (n.includes('sprite')) return '/assets/sprite.jpg';
    if (n.includes('red bull')) return '/assets/Red Bull.jpg';
    if (n.includes('monster energy')) return '/assets/monster energy.jpg';
    if (n.includes('canadian whiskey')) return '/assets/canadian whiskey.jpg';
    if (n.includes('bourbon')) return '/assets/Bourbon.jpg';
    if (n.includes('jameson')) return '/assets/jameson.jpg';
    if (n.includes('jelen')) return '/assets/jelen.jpg';
    if (n.includes('heineken')) return '/assets/heineken.jpg';
    if (n.includes('zajecarsko')) return '/assets/zajecarsko.jpg';
    if (n.includes('paulaner')) return '/assets/Paulaner Munich.jpg';
    if (n.includes('vranac')) return '/assets/Vranac Pro Corde.jpg';
    if (n.includes('chardonnay')) return '/assets/Chardonnay Premium.jpg';
    return null;
  }

  /** Returns a warm gradient for the card image area when no photo is available */
  getProductGradient(name: string): string {
    const n = name.toLowerCase();
    if (n.includes('whiskey') || n.includes('bourbon') || n.includes('jameson')) {
      return 'linear-gradient(135deg, #92400e 0%, #78350f 50%, #451a03 100%)';
    }
    if (n.includes('wine') || n.includes('vranac') || n.includes('chardonnay')) {
      return 'linear-gradient(135deg, #7f1d1d 0%, #9f1239 50%, #4c0519 100%)';
    }
    // Generic gradient for image-based products (image will cover it)
    return 'linear-gradient(135deg, #0f172a 0%, #1e293b 100%)';
  }

  /** Returns an emoji for products without a photo */
  getProductEmoji(name: string): string {
    const n = name.toLowerCase();
    if (n.includes('whiskey') || n.includes('bourbon') || n.includes('jameson')) return '🥃';
    if (n.includes('wine') || n.includes('vranac') || n.includes('chardonnay')) return '🍷';
    return '🍶';
  }
}
