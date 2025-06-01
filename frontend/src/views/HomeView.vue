

<style scoped>
h2 {
  margin-bottom: 10px;
}
</style>
<template>
  <div class="home-container">
    <div class="welcome-card">
      <h2>欢迎来到淘多多商城</h2>
      <p>在这里，您可以浏览最新商品、管理订单、体验便捷购物。</p>
      <ul class="features-list">
        <li>🛒 丰富多样的商品分类</li>
        <li>💳 安全快捷的支付方式</li>
        <li>🚚 极速可靠的物流配送</li>
        <li>🎁 专属优惠与会员特权</li>
      </ul>
      <div class="explore-tip">
        请通过顶部菜单访问各个功能页面，开启您的购物之旅！
      </div>
    </div>
  </div>
</template>

<style scoped>
.home-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 70vh;
  background: linear-gradient(120deg, #f8fafc 0%, #e0e7ff 100%);
}

.welcome-card {
  background: #fff;
  border-radius: 18px;
  box-shadow: 0 4px 24px rgba(60, 72, 88, 0.08);
  padding: 40px 32px 32px 32px;
  max-width: 420px;
  width: 100%;
  text-align: center;
}

h2 {
  color: #3b82f6;
  font-size: 2rem;
  margin-bottom: 16px;
  font-weight: 700;
  letter-spacing: 1px;
}

p {
  color: #374151;
  font-size: 1.1rem;
  margin-bottom: 18px;
}

.features-list {
  list-style: none;
  padding: 0;
  margin: 0 0 18px 0;
  text-align: left;
}

.features-list li {
  font-size: 1rem;
  color: #6366f1;
  margin-bottom: 8px;
  display: flex;
  align-items: center;
  gap: 8px;
}

.explore-tip {
  background: #f1f5f9;
  color: #64748b;
  border-radius: 8px;
  padding: 10px 0;
  font-size: 0.98rem;
  margin-top: 10px;
}
</style>