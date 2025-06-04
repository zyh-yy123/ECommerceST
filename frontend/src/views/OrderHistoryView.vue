<template>
  <div class="orders-container">
    <h2 class="cyber-title">我的订单</h2>
    <el-table :data="orders" style="width: 100%">
      <el-table-column prop="orderNumber" label="订单号"></el-table-column>
      <el-table-column prop="totalAmount" label="总金额" width="120">
        <template #default="scope">
          ￥{{ scope.row.totalAmount.toFixed(2) }}
        </template>
      </el-table-column>
      <el-table-column label="状态" width="120">
        <template #default="scope">
          {{
            scope.row.status === 0
              ? '待支付'
              : scope.row.status === 1
              ? '待发货'
              : scope.row.status === 2
              ? '已发货'
              : scope.row.status === 3
              ? '已完成'
              : '已取消'
          }}
        </template>
      </el-table-column>
      <el-table-column prop="createdAt" label="下单时间" width="180"></el-table-column>
      <el-table-column label="操作" width="150">
        <template #default="scope">
          <el-button
            v-if="scope.row.status === 2"
            type="primary"
            size="mini"
            @click="confirmReceipt(scope.row.id)"
          >
            确认收货
          </el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import http from '../services/http';
import { ElMessage } from 'element-plus';

export default {
  name: 'OrderHistory',
  setup() {
    const orders = ref([]);

    const fetchOrders = async () => {
      try {
        orders.value = await http.get('/api/orders');
      } catch (error) {
        console.error('获取订单列表失败：', error);
      }
    };

    const confirmReceipt = async (orderId) => {
      try {
        await http.post(`/api/orders/${orderId}/complete`);
        ElMessage.success('确认收货成功');
        await fetchOrders();
      } catch (error) {
        console.error('确认收货失败：', error);
      }
    };

    onMounted(() => {
      fetchOrders();
    });

    return { orders, confirmReceipt };
  }
};
</script>

<style scoped>
.orders-container {
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
