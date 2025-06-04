<template>
  <div class="products-container">
    <h2 class="cyber-title">商品列表</h2>
    <el-table :data="products" style="width: 100%">
      <el-table-column prop="id" label="ID" width="80"></el-table-column>
      <el-table-column prop="name" label="名称"></el-table-column>
      <el-table-column prop="price" label="价格" width="100">
        <template #default="scope">
          ￥{{ scope.row.price.toFixed(2) }}
        </template>
      </el-table-column>
      <el-table-column prop="stock" label="库存" width="100"></el-table-column>
      <el-table-column label="操作" width="200">
        <template #default="scope">
          <el-button type="primary" size="mini" @click="viewDetail(scope.row.id)">查看</el-button>
          <el-button type="success" size="mini" @click="addToCart(scope.row.id)">加购物车</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import http from '../services/http';

export default {
  name: 'ProductList',
  setup() {
    const products = ref([]);

    const fetchProducts = async () => {
      try {
        const res = await http.get('/api/products');
        products.value = res;
      } catch (error) {
        console.error('获取商品失败：', error);
      }
    };

    const viewDetail = id => {
      // 跳转到商品详情页，使用路由
      window.location.href = `/product/${id}`;
    };

    const addToCart = async id => {
      try {
        await http.post('/api/cart/add', { productId: id, quantity: 1 });
        window.alert('添加购物车成功');
      } catch (error) {
        console.error('添加购物车失败：', error);
      }
    };

    onMounted(() => {
      fetchProducts();
    });

    return { products, viewDetail, addToCart };
  }
};
</script>

<style scoped>
.products-container {
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
</style>
