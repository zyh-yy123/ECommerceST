<template>
  <div class="checkout-page-container">
    <h1 class="cyber-title">订单支付</h1>
      
    <div class="cyber-card order-summary">
      <h2>订单信息</h2>
      <div class="summary-row">
        <span>订单号：</span>
        <span>{{ order.orderNumber }}</span>
      </div>
      <div class="summary-row">
        <span>总金额：</span>
        <span class="total-price">¥{{ order.totalAmount }}</span>
      </div>
    </div>

    <div class="cyber-card payment-methods">
      <h2>选择支付方式</h2>
      <div class="payment-options">
        <div 
          v-for="method in paymentMethods" 
          :key="method.id"
          class="payment-option"
          :class="{ active: selectedPayment === method.id }"
          @click="selectPayment(method.id)"
        >
          <img :src="method.icon" :alt="method.name">
          <span>{{ method.name }}</span>
        </div>
      </div>
    </div>

    <div class="payment-actions">
      <button 
        class="cyber-button pay-btn"
        @click="handlePayment"
        :disabled="!selectedPayment || isProcessing"
      >
        立即支付
      </button>
    </div>

    <!-- 支付加载动画 -->
    <div v-if="isProcessing" class="payment-overlay">
      <div class="cyber-loader"></div>
      <div class="processing-text">支付处理中...</div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'CheckoutPage',
  data() {
    return {
      order: {
        orderNumber: '',
        totalAmount: 0
      },
      cartItemCount: 0,
      selectedPayment: null,
      isProcessing: false,
      paymentMethods: [
        {
          id: 'wechat',
          name: '微信支付',
          icon: '/images/wechat-pay.png'
        },
        {
          id: 'alipay',
          name: '支付宝',
          icon: '/images/alipay.png'
        },
        {
          id: 'unionpay',
          name: '银联支付',
          icon: '/images/unionpay.png'
        }
      ]
    }
  },
  async created() {
    // 从路由参数获取订单信息
    const orderId = this.$route.params.orderId;
    await this.fetchOrderDetails(orderId);
  },
  methods: {
    async fetchOrderDetails(orderId) {
      try {
        const response = await fetch(`/api/orders/${orderId}`, {
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });
        if (!response.ok) throw new Error('获取订单详情失败');
        this.order = await response.json();
      } catch (error) {
        console.error('Error fetching order details:', error);
      }
    },
    selectPayment(methodId) {
      this.selectedPayment = methodId;
    },
    async handlePayment() {
      if (!this.selectedPayment) return;
      
      this.isProcessing = true;
      
      try {
        // 调用支付API
        const response = await fetch(`/api/payment/${this.order.id}`, {
          method: 'POST',
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}`
          }
        });

        if (!response.ok) throw new Error('支付失败');
        
        const result = await response.json();
        if (result.success) {
          // 支付成功后跳转到订单列表
          this.$router.push('/orders');
        } else {
          throw new Error(result.message || '支付失败');
        }
      } catch (error) {
        console.error('Error processing payment:', error);
        alert(error.message);
      } finally {
        this.isProcessing = false;
      }
    }
  }
}
</script>

<style scoped>
.checkout-page-container {
  padding: 20px;
  max-width: 800px;
  margin: 0 auto;
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

.cyber-card {
  background: rgba(20, 20, 20, 0.9);
  border: 1px solid var(--border-color);
  box-shadow: 0 0 15px rgba(0, 255, 157, 0.2);
  padding: 20px;
  border-radius: 8px;
}

.order-summary,
.payment-methods {
  margin-bottom: 20px;
}

.order-summary h2,
.payment-methods h2 {
  color: var(--secondary-color);
  margin-bottom: 15px;
  font-size: 20px;
}

.summary-row {
  display: flex;
  justify-content: space-between;
  margin: 10px 0;
  color: var(--text-color);
}

.total-price {
  color: var(--primary-color);
  font-size: 1.5em;
  text-shadow: 0 0 8px rgba(0, 255, 157, 0.7);
  font-weight: bold;
}

.payment-options {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(150px, 1fr));
  gap: 20px;
  margin-top: 20px;
}

.payment-option {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 10px;
  padding: 20px;
  border: 1px solid var(--secondary-color);
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.3s;
  background: rgba(30, 30, 30, 0.8);
}

.payment-option:hover,
.payment-option.active {
  background: rgba(0, 184, 255, 0.3);
  box-shadow: 0 0 15px rgba(0, 184, 255, 0.6);
}

.payment-option img {
  width: 50px;
  height: 50px;
  object-fit: contain;
}

.payment-option span {
  color: var(--text-color);
  font-size: 1em;
  font-weight: bold;
}

.payment-actions {
  display: flex;
  justify-content: center;
  margin-top: 30px;
}

.cyber-button {
  background: linear-gradient(45deg, var(--primary-color), var(--secondary-color));
  border: none;
  color: #000;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 1px;
  transition: all 0.3s ease;
  padding: 12px 25px;
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

.pay-btn {
  width: 200px;
  background: linear-gradient(45deg, var(--secondary-color), var(--primary-color));
}

.pay-btn:hover {
   box-shadow: 0 0 15px rgba(0, 184, 255, 0.5));
}

.payment-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.8);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 20px;
  z-index: 1000;
}

.processing-text {
  color: var(--primary-color);
  font-size: 1.5em;
  text-shadow: 0 0 10px rgba(0, 255, 157, 0.7));
}

@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}

.cyber-loader {
  width: 60px;
  height: 60px;
  border: 4px solid var(--secondary-color);
  border-radius: 50%;
  border-top-color: transparent;
  animation: spin 1s linear infinite;
}

</style> 