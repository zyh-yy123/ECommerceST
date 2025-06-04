<template>
  <div class="cart-container">
    <h2 class="cyber-title">购物车</h2>
    <el-table :data="cartItems" style="width: 100%">
      <el-table-column prop="product.name" label="商品" />
      <el-table-column prop="product.price" label="价格" width="100">
        <template #default="scope">
          ￥{{ scope.row.product.price.toFixed(2) }}
        </template>
      </el-table-column>
      <el-table-column label="数量" width="120">
        <template #default="scope">
          <el-input-number
            v-model="scope.row.quantity"
            :min="1"
            :max="scope.row.product.stock"
            @change="updateCart(scope.row)"
          ></el-input-number>
        </template>
      </el-table-column>
      <el-table-column label="小计" width="120">
        <template #default="scope">
          ￥{{ (scope.row.quantity * scope.row.product.price).toFixed(2) }}
        </template>
      </el-table-column>
      <el-table-column label="操作" width="120">
        <template #default="scope">
          <el-button type="danger" size="mini" @click="removeFromCart(scope.row.id)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <div class="cart-summary">
      <h3>总计：￥{{ totalAmount.toFixed(2) }}</h3>
      <el-button type="primary" @click="goToCheckout">去结算</el-button>
    </div>
  </div>
</template>

<script>
import { ref, onMounted, computed } from 'vue';
import http from '../services/http';
import { ElMessage } from 'element-plus';

export default {
  name: 'CartView',
  setup() {
    const cartItems = ref([]);

    const fetchCart = async () => {
      try {
        const res = await http.get('/api/cart');
        cartItems.value = res;
      } catch (error) {
        console.error('获取购物车失败：', error);
      }
    };

    const updateCart = async item => {
      try {
        await http.put(`/api/cart/update/${item.id}`, { quantity: item.quantity });
        ElMessage.success('更新成功');
        await fetchCart();
      } catch (error) {
        console.error('更新购物车失败：', error);
      }
    };

    const removeFromCart = async id => {
      try {
        await http.delete(`/api/cart/remove/${id}`);
        ElMessage.success('删除成功');
        await fetchCart();
      } catch (error) {
        console.error('删除购物车项失败：', error);
      }
    };

    const totalAmount = computed(() => {
      return cartItems.value.reduce((sum, item) => sum + item.quantity * item.product.price, 0);
    });

    const goToCheckout = () => {
      window.location.href = '/checkout';
    };

    onMounted(() => {
      fetchCart();
    });

    return { cartItems, totalAmount, updateCart, removeFromCart, goToCheckout };
  }
};
</script>

<style scoped>
.cart-container {
  padding: 20px;
}

.cyber-title {
  color: var(--primary-color);
  margin-bottom: 20px;
  font-size: 24px;
  text-transform: uppercase;
  letter-spacing: 2px;
  text-shadow: 0 0 10px rgba(0, 255, 157, 0.5);
}

.cart-summary {
  margin-top: 20px;
  text-align: right;
  padding: 20px;
  background: rgba(20, 20, 20, 0.9);
  border: 1px solid var(--border-color);
  border-radius: 4px;
}

.cart-summary h3 {
  color: var(--primary-color);
  margin-bottom: 15px;
}
</style>
