<template>
  <div style="max-width: 400px; margin: 40px auto;">
    <h2>用户登录</h2>
    <el-form :model="form" :rules="rules" ref="loginForm" label-width="80px">
      <el-form-item label="用户名" prop="username">
        <el-input v-model="form.username"></el-input>
      </el-form-item>
      <el-form-item label="密码" prop="password">
        <el-input type="password" v-model="form.password"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="handleLogin">登录</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { ref } from 'vue';
import http from '../services/http';

export default {
  name: 'LogininView',
  setup() {
    const form = ref({
      username: '',
      password: ''
    });
    const rules = {
      username: [{ required: true, message: '请输入用户名', trigger: 'blur' }],
      password: [{ required: true, message: '请输入密码', trigger: 'blur' }]
    };
    const loginForm = ref(null);

    const handleLogin = () => {
      loginForm.value.validate(async valid => {
        if (!valid) return;
        try {
          const res = await http.post('/User/login', form.value);
          console.log('登录成功，用户信息：', res);
          // 这里暂时不做 Token 处理，仅记录并跳转
          localStorage.setItem('user', JSON.stringify(res)); 
          // 跳转到商品列表页
          window.location.href = '/products';
        } catch (error) {
          console.error('登录失败：', error);
        }
      });
    };

    return { form, rules, loginForm, handleLogin };
  }
};
</script>

<style scoped>
h2 {
  text-align: center;
  margin-bottom: 20px;
}
</style>
