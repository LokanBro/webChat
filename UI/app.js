const routes=[
    {path:'/singIn',component:singIn},
    {path:'/AdminView',component:AdminView},
    {
        path: '/ChatRoom/:userLogin/:chatName',
        name: 'ChatRoom',
        component: ChatRoom
    },
]

const router=new VueRouter({
    routes
})

const app = new Vue({
    router
}).$mount('#app')