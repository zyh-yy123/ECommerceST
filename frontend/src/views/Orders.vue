<template>
  <div class="orders-page-container">
    <h1 class="cyber-title">订单历史</h1>
      
    <div v-if="loading" class="cyber-loader"></div>
    
    <div v-else-if="orders.length === 0" class="cyber-card empty-orders">
      <h3>暂无订单记录</h3>
      <router-link to="/products" class="cyber-button">去购物</router-link>
    </div>
    
    <div v-else class="orders-list">
      <div v-for="order in orders" :key="order.id" class="cyber-card order-card">
        <div class="order-header">
          <div class="order-info">
            <h3>订单号：{{ order.orderNumber }}</h3>
            <p class="order-date">{{ formatDate(order.createdAt) }}</p>
          </div>
          <div class="order-status" :class="order.status.toLowerCase()">
            {{ order.status }}
          </div>
        </div>
        
        <div class="order-items">
          <div v-for="item in order.orderItems" :key="item.id" class="order-item">
            <img :src="item.product.thumbnailUrl" :alt="item.product.name" class="item-image">
            <div class="item-details">
              <h4>{{ item.product.name }}</h4>
              <p class="item-price">¥{{ item.unitPrice }} × {{ item.quantity }}</p>
            </div>
          </div>
        </div>
        
        <div class="order-footer">
          <div class="order-total">
            总计：<span class="total-price">¥{{ calculateTotal(order) }}</span>
          </div>
          <div class="order-actions">
            <button 
              v-if="order.status === 'PENDING'"
              class="cyber-button cancel-btn"
              @click="cancelOrder(order)"
            >
              取消订单
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'OrderList',
  data() {
    return {
      orders: [],
      loading: false,
      cartItemCount: 0
    }
  },
  async created() {
    await this.fetchOrders();
  },
  methods: {
    async fetchOrders() {
      try {
        this.loading = true;
        const response = await fetch('/api/orders', {
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });
        if (!response.ok) throw new Error('获取订单列表失败');
        this.orders = await response.json();
      } catch (error) {
        console.error('Error fetching orders:', error);
      } finally {
        this.loading = false;
      }
    },
    formatDate(dateString) {
      const date = new Date(dateString);
      return date.toLocaleString('zh-CN', {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit'
      });
    },
    calculateTotal(order) {
      return order.orderItems.reduce((sum, item) => sum + item.unitPrice * item.quantity, 0);
    },
    async cancelOrder(order) {
      try {
        const response = await fetch(`/api/orders/${order.id}/cancel`, {
          method: 'POST',
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });

        if (!response.ok) throw new Error('取消订单失败');
        
        await this.fetchOrders();
      } catch (error) {
        console.error('Error canceling order:', error);
      }
    }
  }
}
</script>

<style scoped>
.orders-page-container {
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

.empty-orders {
  text-align: center;
  padding: 40px;
}

.empty-orders h3 {
  color: var(--secondary-color);
  margin-bottom: 20px;
}

.orders-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.order-card {
  padding: 20px;
}

.order-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 15px;
  padding-bottom: 15px;
  border-bottom: 1px solid var(--secondary-color);
}

.order-date {
  color: var(--text-color);
  font-size: 0.9em;
}

.order-status {
  padding: 5px 10px;
  border-radius: 5px;
  font-size: 0.9em;
  text-transform: uppercase;
  font-weight: bold;
}

.order-status.pending {
  background: var(--primary-color);
  color: #0a0a0a;
}

.order-status.completed {
  background: var(--secondary-color);
  color: #0a0a0a;
}

.order-status.cancelled {
  background: #e74c3c;
  color: #0a0a0a;
}

.order-items {
  display: flex;
  flex-direction: column;
  gap: 10px;
  margin-bottom: 15px;
}

.order-item {
  display: flex;
  align-items: center;
  gap: 15px;
}

.item-image {
  width: 60px;
  height: 60px;
  object-fit: cover;
  border: 1px solid var(--secondary-color);
}

.item-details {
  flex-grow: 1;
}

.item-details h4 {
  color: var(--secondary-color);
  font-size: 16px;
  margin-bottom: 5px;
}

.item-price {
  color: var(--text-color);
  font-size: 0.9em;
}

.order-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-top: 15px;
  border-top: 1px solid var(--secondary-color));
}

.order-total {
  color: var(--text-color);
}

.total-price {
  color: var(--primary-color);
  font-size: 1.2em;
  text-shadow: 0 0 8px rgba(0, 255, 157, 0.7));
  font-weight: bold;
}

.order-actions {
  display: flex;
  gap: 10px;
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

.cancel-btn {
  background: #e74c3c;
  border-color: #e74c3c;
}

.cancel-btn:hover {
   box-shadow: 0 0 15px rgba(231, 76, 60, 0.5));
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