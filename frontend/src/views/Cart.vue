<template>
  <div class="cart-page-container">
    <h1 class="cyber-title">购物车</h1>
      
    <div v-if="loading" class="cyber-loader"></div>
    
    <div v-else-if="cartItems.length === 0" class="cyber-card empty-cart">
      <h3>购物车是空的</h3>
      <router-link to="/products" class="cyber-button">去购物</router-link>
    </div>
    
    <div v-else>
      <div class="cart-items">
        <div v-for="item in cartItems" :key="item.id" class="cyber-card cart-item">
          <img :src="item.product.thumbnailUrl" :alt="item.product.name" class="item-image">
          <div class="item-details">
            <h3>{{ item.product.name }}</h3>
            <p class="item-price">¥{{ item.product.price }}</p>
          </div>
          <div class="quantity-controls">
            <button 
              class="cyber-button quantity-btn"
              @click="updateQuantity(item, item.quantity - 1)"
              :disabled="item.quantity <= 1"
            >-</button>
            <span class="quantity">{{ item.quantity }}</span>
            <button 
              class="cyber-button quantity-btn"
              @click="updateQuantity(item, item.quantity + 1)"
              :disabled="item.quantity >= item.product.stock"
            >+</button>
          </div>
          <button 
            class="cyber-button remove-btn"
            @click="removeItem(item)"
          >
            删除
          </button>
        </div>
      </div>

      <div class="cart-summary cyber-card">
        <div class="summary-row">
          <span>商品总数：</span>
          <span>{{ totalItems }} 件</span>
        </div>
        <div class="summary-row">
          <span>总价：</span>
          <span class="total-price">¥{{ totalPrice }}</span>
        </div>
        <button 
          class="cyber-button checkout-btn"
          @click="checkout"
          :disabled="loading"
        >
          结算
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ShoppingCart',
  data() {
    return {
      cartItems: [],
      loading: false,
      cartItemCount: 0
    }
  },
  computed: {
    totalItems() {
      return this.cartItems.reduce((sum, item) => sum + item.quantity, 0);
    },
    totalPrice() {
      return this.cartItems.reduce((sum, item) => sum + item.product.price * item.quantity, 0);
    }
  },
  async created() {
    await this.fetchCartItems();
  },
  methods: {
    async fetchCartItems() {
      try {
        this.loading = true;
        const response = await fetch('/api/cart', {
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });
        if (!response.ok) throw new Error('获取购物车失败');
        this.cartItems = await response.json();
        this.cartItemCount = this.totalItems;
      } catch (error) {
        console.error('Error fetching cart:', error);
      } finally {
        this.loading = false;
      }
    },
    async updateQuantity(item, newQuantity) {
      try {
        const response = await fetch(`/api/cart/update/${item.id}`, {
          method: 'PUT',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          },
          body: JSON.stringify({ quantity: newQuantity })
        });

        if (!response.ok) throw new Error('更新数量失败');
        
        item.quantity = newQuantity;
        this.cartItemCount = this.totalItems;
      } catch (error) {
        console.error('Error updating quantity:', error);
      }
    },
    async removeItem(item) {
      try {
        const response = await fetch(`/api/cart/remove/${item.id}`, {
          method: 'DELETE',
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });

        if (!response.ok) throw new Error('删除商品失败');
        
        this.cartItems = this.cartItems.filter(i => i.id !== item.id);
        this.cartItemCount = this.totalItems;
      } catch (error) {
        console.error('Error removing item:', error);
      }
    },
    async checkout() {
      try {
        this.loading = true;
        const response = await fetch('/api/orders/create', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });

        if (!response.ok) throw new Error('创建订单失败');
        
        const order = await response.json();
        this.$router.push(`/checkout/${order.id}`);
      } catch (error) {
        console.error('Error checking out:', error);
      } finally {
        this.loading = false;
      }
    }
  }
}
</script>

<style scoped>
.cart-page-container {
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

.cyber-loader {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 200px;
}

.cyber-card {
  background: rgba(20, 20, 20, 0.9);
  border: 1px solid var(--border-color);
  box-shadow: 0 0 15px rgba(0, 255, 157, 0.2);
  padding: 20px;
  border-radius: 8px;
}

.empty-cart {
  text-align: center;
  padding: 40px;
}

.empty-cart h3 {
  color: var(--secondary-color);
  margin-bottom: 20px;
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

.cart-items {
  display: flex;
  flex-direction: column;
  gap: 15px;
  margin-bottom: 20px;
}

.cart-item {
  display: flex;
  align-items: center;
  gap: 20px;
}

.item-image {
  width: 80px;
  height: 80px;
  object-fit: cover;
  border: 2px solid var(--secondary-color);
  border-radius: 4px;
}

.item-details {
  flex-grow: 1;
}

.item-details h3 {
  color: var(--secondary-color);
  font-size: 18px;
  margin-bottom: 5px;
}

.item-price {
  color: var(--primary-color);
  font-size: 1.1em;
}

.quantity-controls {
  display: flex;
  align-items: center;
  gap: 5px;
}

.quantity-btn {
  padding: 5px 10px;
  font-size: 1em;
}

.quantity {
  color: var(--text-color);
  min-width: 20px;
  text-align: center;
}

.remove-btn {
  background: #e74c3c;
  border-color: #e74c3c;
}

.remove-btn:hover {
   box-shadow: 0 0 15px rgba(231, 76, 60, 0.5);
}

.cart-summary {
  margin-top: 20px;
  padding: 20px;
}

.summary-row {
  display: flex;
  justify-content: space-between;
  margin-bottom: 10px;
  color: var(--text-color);
}

.total-price {
  color: var(--primary-color);
  font-size: 1.5em;
  text-shadow: 0 0 8px rgba(0, 255, 157, 0.7);
  font-weight: bold;
}

.checkout-btn {
  width: 100%;
  margin-top: 20px;
  background: linear-gradient(45deg, #00b8ff, #00ff9d);
  border: none;
  color: #000;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 1px;
}

.checkout-btn:hover {
  box-shadow: 0 0 15px rgba(0, 184, 255, 0.5);
}

@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}

.cyber-loader {
  width: 50px;
  height: 50px;
  border: 3px solid var(--primary-color);
  border-radius: 50%;
  border-top-color: transparent;
  animation: spin 1s linear infinite;
}

</style> 