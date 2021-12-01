import { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/Index.vue') },
      { path: 'sign-in', component: () => import('src/pages/user/SignIn.vue')},
      { path: 'sign-up', component: () => import('src/pages/user/SignUp.vue')},
      { path: 'forgot-password', component: () => import('src/pages/user/ForgotPassword.vue')},
      { path: 'search-food', component: () => import('src/pages/site/SearchFood.vue')},
      { path: 'search-food-dialog', component: () => import('src/components/search-food/DetailDialog.vue')},
      { path: 'add-to-menu', component: () => import('src/components/search-food/AddToMenu.vue')},
      { path: 'select-schedule', component: () => import('src/components/schedule/Select.vue')},
      { path: 'eating-schedule', component: () => import('src/components/eating-schedule/EatingSchedule.vue')},
    ],
  },


  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/Error404.vue'),
  },
];

export default routes;
