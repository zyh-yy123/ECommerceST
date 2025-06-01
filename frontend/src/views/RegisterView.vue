<template>
  <div class="register-container">
    <div class="register-card">
      <div class="register-header">
        <img src="https://cdn.jsdelivr.net/gh/twitter/twemoji@14.0.2/assets/72x72/1f389.png" class="register-emoji" alt="party" />
        <h2>欢迎加入！</h2>
        <p class="subtitle">开启你的精彩之旅 🎉</p>
      </div>
      <el-form :model="form" :rules="rules" ref="registerForm" label-width="80px">
        <el-form-item label="用户名" prop="username">
          <el-input v-model="form.username" placeholder="起个有趣的名字吧~"></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input type="password" v-model="form.password" placeholder="设置你的密码"></el-input>
        </el-form-item>
        <el-form-item label="手机号" prop="phoneNumber">
          <el-input v-model="form.phoneNumber" placeholder="请输入手机号"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" class="register-btn" @click="handleRegister">立即注册 🚀</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';
import http from '../services/http';

export default {
  name: 'RegisterView',
  setup() {
    const form = ref({
      username: '',
      password: '',
      phoneNumber: ''
    });
    const rules = {
      username: [{ required: true, message: '请输入用户名', trigger: 'blur' }],
      password: [{ required: true, message: '请输入密码', trigger: 'blur' }],
      phoneNumber: [{ required: true, message: '请输入手机号', trigger: 'blur' }]
    };
    const registerForm = ref(null);

    const handleRegister = () => {
      registerForm.value.validate(async valid => {
        if (!valid) return;
        try {
          await http.post('/User/register', form.value);
          window.location.href = '/login';
        } catch (error) {
          // 可以弹窗提示
        }
      });
    };

    return { form, rules, registerForm, handleRegister };
  }
};
</script>

<style scoped>
.register-container {
  min-height: 100vh;
  background: linear-gradient(135deg, #f9d423 0%, #ff4e50 100%);
  display: flex;
  align-items: center;
  justify-content: center;
}
.register-card {
  background: #fff;
  border-radius: 18px;
  box-shadow: 0 8px 32px 0 rgba(255, 78, 80, 0.15);
  padding: 36px 32px 24px 32px;
  width: 350px;
  animation: pop-in 0.7s cubic-bezier(.68,-0.55,.27,1.55);
}
@keyframes pop-in {
  0% { transform: scale(0.7); opacity: 0; }
  100% { transform: scale(1); opacity: 1; }
}
.register-header {
  text-align: center;
  margin-bottom: 18px;
}
.register-emoji {
  width: 48px;
  height: 48px;
  margin-bottom: 8px;
}
h2 {
  margin-bottom: 4px;
  font-weight: bold;
  color: #ff4e50;
  font-size: 1.7rem;
}
.subtitle {
  color: #ffb347;
  font-size: 1rem;
  margin-bottom: 0;
}
.el-form-item__label {
  color: #ff4e50;
  font-weight: 500;
}
.register-btn {
  width: 100%;
  background: linear-gradient(90deg, #f9d423 0%, #ff4e50 100%);
  border: none;
  font-weight: bold;
  font-size: 1.1rem;
  letter-spacing: 1px;
}
.register-btn:hover {
  filter: brightness(1.08);
}
</style>
