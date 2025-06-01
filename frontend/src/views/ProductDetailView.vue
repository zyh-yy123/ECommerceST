<template>
  <div style="padding: 20px;">
    <h2>商品详情</h2>
    <el-card v-if="product" style="max-width: 600px; margin: 0 auto;">
      <h3>{{ product.name }}</h3>
      <p>{{ product.description }}</p>
      <p>价格：￥{{ product.price.toFixed(2) }}</p>
      <p>库存：{{ product.stock }}</p>
      <div style="margin: 20px 0;">
        <el-input-number v-model="quantity" :min="1" :max="product.stock"></el-input-number>
        <el-button type="success" style="margin-left: 20px;" @click="addToCart">加入购物车</el-button>
      </div>
    </el-card>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import http from '../services/http';

export default {
  name: 'ProductDetail',
  setup() {
    const route = useRoute();
    const productId = Number(route.params.id);
    const product = ref(null);
    const quantity = ref(1);

    const fetchProduct = async () => {
      try {
        const res = await http.get(`/Product/${productId}`); // 对应后端 GET /api/Product/{id}
        product.value = res;
      } catch (error) {
        console.error('获取商品详情失败：', error);
      }
    };

    const addToCart = async () => {
      if (!product.value) return;
      try {
        await http.post('/Cart', { userId: 1, productId, quantity: quantity.value });
        window.alert('添加购物车成功');
      } catch (error) {
        console.error('添加购物车失败：', error);
      }
    };

    onMounted(() => {
      fetchProduct();
    });

    return { product, quantity, addToCart };
  }
};
</script>

<style scoped>
h2 {
  margin-bottom: 20px;
}
</style>
