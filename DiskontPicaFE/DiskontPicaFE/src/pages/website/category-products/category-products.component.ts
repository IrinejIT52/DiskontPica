import { CommonModule, DecimalPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Product } from '../../../models/product';
import { ProductService } from '../../../services/product.service';
import { CartService } from '../../../services/cart.service';
import { MatButtonModule } from '@angular/material/button';

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

  constructor(private productService: ProductService, private cartService: CartService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.productService.GetAllProducts().subscribe(data => {
      this.allProducts = data;
      this.filteredProducts = data;
    });
  }

  applyFilter(event: any) {
    const term = (event.target.value || '').trim().toLowerCase();
    this.filteredProducts = this.allProducts.filter(p =>
      p.name.toLowerCase().includes(term) ||
      p.description.toLowerCase().includes(term)
    );
  }

  addToCart(product: Product) {
    this.cartService.addToCart(product);
  }

  /** Returns a local asset path for a product image, or null to use the emoji fallback */
  getProductImage(name: string): string | null {
    const n = name.toLowerCase();
    if (n.includes('water') || n.includes('vivia') || n.includes('prolom')) {
      return '/assets/product_water.png';
    }
    if (n.includes('cola') || n.includes('fanta') || n.includes('sprite')) {
      return '/assets/product_cola.png';
    }
    if (n.includes('red bull') || n.includes('monster') || n.includes('energy')) {
      return '/assets/product_energy.png';
    }
    if (n.includes('beer') || n.includes('jelen') || n.includes('heineken') ||
        n.includes('zajecarsko') || n.includes('paulaner')) {
      return '/assets/product_beer.png';
    }
    // Whiskey and wine — no image generated, fall through to emoji
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
