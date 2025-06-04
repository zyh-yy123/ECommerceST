<template>
  <div class="products-page-container">
    <h1 class="cyber-title">商品列表</h1>
      
    <div class="products-grid">
      <div v-for="product in products" :key="product.id" class="cyber-card product-card">
        <img :src="product.thumbnailUrl" :alt="product.name" class="product-image">
        <h3>{{ product.name }}</h3>
        <p class="product-description">{{ product.description }}</p>
        <div class="product-price">¥{{ product.price }}</div>
        <button 
          class="cyber-button"
          @click="addToCart(product)"
          :disabled="product.stock <= 0"
        >
          {{ product.stock > 0 ? '加入购物车' : '已售罄' }}
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ProductList',
  data() {
    return {
      products: [],
      cartItemCount: 0,
      loading: false
    }
  },
  async created() {
    await this.fetchProducts();
    await this.fetchCartCount();
  },
  methods: {
    async fetchProducts() {
      try {
        this.loading = true;
        const response = await fetch('/api/products');
        if (!response.ok) throw new Error('获取商品列表失败');
        this.products = await response.json();
      } catch (error) {
        console.error('Error fetching products:', error);
      } finally {
        this.loading = false;
      }
    },
    async fetchCartCount() {
      try {
        const response = await fetch('/api/cart/count', {
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });
        if (!response.ok) throw new Error('获取购物车数量失败');
        const data = await response.json();
        this.cartItemCount = data.count;
      } catch (error) {
        console.error('Error fetching cart count:', error);
      }
    },
    async addToCart(product) {
      try {
        const response = await fetch('/api/cart/add', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          },
          body: JSON.stringify({
            productId: product.id,
            quantity: 1
          })
        });

        if (!response.ok) throw new Error('添加商品到购物车失败');
        
        await this.fetchCartCount();
      } catch (error) {
        console.error('Error adding to cart:', error);
      }
    }
  }
}
</script>

<style scoped>
.products-page-container {
  padding: 20px;
}

.cyber-title {
  color: var(--primary-color);
  font-size: 28px;
  text-transform: uppercase;
  letter-spacing: 3px;
  text-shadow: 0 0 15px rgba(0, 255, 157, 0.7);
  margin-bottom: 30px;
  text-align: center;
}

.products-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 25px;
}

.product-card {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  background: rgba(20, 20, 20, 0.9);
  border: 1px solid var(--border-color);
  box-shadow: 0 0 15px rgba(0, 255, 157, 0.2);
  padding: 20px;
  border-radius: 8px;
  transition: all 0.3s ease;
}

.product-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 0 25px rgba(0, 255, 157, 0.4);
}

.product-image {
  width: 100%;
  height: 220px;
  object-fit: cover;
  margin-bottom: 15px;
  border: 2px solid var(--secondary-color);
  border-radius: 4px;
}

.product-card h3 {
  color: var(--secondary-color);
  font-size: 20px;
  margin-bottom: 10px;
}

.product-description {
  color: var(--text-color);
  margin: 10px 0;
  flex-grow: 1;
  font-size: 14px;
  line-height: 1.5;
}

.product-price {
  color: var(--primary-color);
  font-size: 1.5em;
  margin: 15px 0;
  text-shadow: 0 0 8px rgba(0, 255, 157, 0.7);
  font-weight: bold;
}

.cyber-button {
  background: linear-gradient(45deg, var(--primary-color), var(--secondary-color));
  border: none;
  color: #000;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 1px;
  transition: all 0.3s ease;
  padding: 10px 20px;
  border-radius: 4px;
  cursor: pointer;
}

.cyber-button:hover {
  transform: translateY(-2px);
  box-shadow: 0 0 15px rgba(0, 255, 157, 0.5);
}

.cyber-button:disabled {
  background: #333;
  color: #888;
  cursor: not-allowed;
  box-shadow: none;
  transform: none;
}
</style> 