<template>
  <div class="checkout-container">
    <h2 class="cyber-title">结算</h2>
    <el-form :model="order" :rules="rules" ref="orderForm" label-width="100px" class="checkout-form">
      <el-form-item label="收货人" prop="name">
        <el-input v-model="order.name"></el-input>
      </el-form-item>
      <el-form-item label="地址" prop="address">
        <el-input v-model="order.address"></el-input>
      </el-form-item>
      <el-form-item label="手机号" prop="phone">
        <el-input v-model="order.phone"></el-input>
      </el-form-item>
      <el-form-item label="备注">
        <el-input type="textarea" v-model="order.remark"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="submitOrder">提交订单</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import http from '../services/http';

export default {
  name: 'CheckOut',
  setup() {
    const orderForm = ref(null);
    const order = ref({
      userId: 1,
      name: '',
      address: '',
      phone: '',
      remark: '',
      items: []
    });
    const rules = {
      name: [{ required: true, message: '请输入收货人姓名', trigger: 'blur' }],
      address: [{ required: true, message: '请输入收货地址', trigger: 'blur' }],
      phone: [{ required: true, message: '请输入手机号', trigger: 'blur' }]
    };

    const loadCartItems = async () => {
      try {
        const cartItems = await http.get('/api/cart');
        order.value.items = cartItems.map(item => ({
          productId: item.product.id,
          quantity: item.quantity
        }));
      } catch (error) {
        console.error('加载购物车失败：', error);
      }
    };

    const submitOrder = () => {
      orderForm.value.validate(async valid => {
        if (!valid) return;
        try {
          const res = await http.post('/api/orders/create', order.value);
          window.alert('下单成功，订单号：' + res.orderNumber);
          window.location.href = '/orders';
        } catch (error) {
          console.error('下单失败：', error);
        }
      });
    };

    onMounted(() => {
      loadCartItems();
    });

    return { order, rules, orderForm, submitOrder };
  }
};
</script>

<style scoped>
.checkout-container {
  padding: 20px;
  max-width: 800px;
  margin: 0 auto;
}

.cyber-title {
  color: var(--primary-color);
  margin-bottom: 20px;
  font-size: 24px;
  text-transform: uppercase;
  letter-spacing: 2px;
  text-shadow: 0 0 10px rgba(0, 255, 157, 0.5);
  text-align: center;
}

.checkout-form {
  background: rgba(20, 20, 20, 0.9);
  padding: 30px;
  border-radius: 8px;
  border: 1px solid var(--border-color);
}
</style>
