const singIn={template:`

<div>
    <button type="button" class="btn btn-primary m-2 float-end" data-bs-toggle="modal" data-bs-target="#exampleModalCreateUser" @click="createClick()">Create User</button>
    <button type="button" class="btn btn-primary m-2 float-end" data-bs-toggle="modal" data-bs-target="#exampleModalLogin" @click="loginClick()">Login</button>

    <table class="table table-striped">
    </table>

    <div class="modal fade" id="exampleModalCreateUser" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-centered">
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">{{modalTitle}}</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
            <div class="input-group mb-3">
                <span class="input-group-text">Login</span>
                <input type="text" class="form-control" v-model="UserLogin">
                <span class="input-group-text">Password</span>
                <input type="text" class="form-control" v-model="UserPassword">
            </div>
            <button type="button" @click="confirmCreateClick()" v-if="UserId==0" class="btn btn-primary">Confirm Creating</button>
        </div>
    </div>
</div>
    </div>

    <div class="modal fade" id="exampleModalLogin" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabelLogin">Login</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="input-group mb-3">
                        <span class="input-group-text">Login</span>
                        <input type="text" class="form-control" v-model="UserLogin">
                        <span class="input-group-text">Password</span>
                        <input type="text" class="form-control" v-model="UserPassword">
                        <span class="input-group-text">ChatName</span>
                        <input type="text" class="form-control" v-model="ChatName">
                    </div>
                    <button type="button" @click="loginConfirmClick()" class="btn btn-primary">Login</button>
                </div>
            </div>
        </div>
    </div>
</div>
`,
data(){
    return{
        Users:[],
        modalTitle:"",
        UserLogin:"",
        UserPassword:"",
        ChatName:"",
        UserBan:false,
        UserId:0,
        Chats:[],
    }
},
methods:{
    refreshUserData(){
        axios.get(variables.API_URL+"User")
        .then((response)=>{
            this.Users=response.data;
        })
    }, 
    refreshChatData(){
        axios.get(variables.API_URL+"Chat")
        .then((response)=>{
            this.Chats=response.data;
        });
    },
    createClick(){
        this.modalTitle="Create User";
        this.UserId=0;
        this.UserLogin="";
        this.UserPassword="";
    },
    loginClick(){
        this.modalTitle="Autorisation";
        this.UserLogin="";
        this.UserPassword="";
        this.ChatName="";
    },
    confirmCreateClick(){
        axios.post(variables.API_URL+"User",{
            UserLogin:this.UserLogin,
            UserPassword:this.UserPassword
        })
        .then((response)=>{
            this.refreshUserData();
            alert(response.data);
        });
    },
    loginConfirmClick(){
        if (!this.userfind()){
            this.createClick();
            return;
        }
        if(!this.chatfind){
            this.createChat(this.ChatName);
            return;
        }
        this.$router.push('/ChatRoom/' + this.UserLogin + "/" + this.ChatName);
    },
    userfind: function() {
        return this.Users.find((user) => {
            return !user.UserBan && user.UserLogin === this.UserLogin && user.UserPassword === this.UserPassword;
        });
    },
    chatfind: function() {
        return this.Chats.find((chat) => {
            return chat.ChatName === this.ChatName;
        });
    },
    createChat(){
        axios.post(variables.API_URL+"Chat",{
            ChatName:this.ChatName
        })
        .then((response)=>{
            this.refreshChatData();
            alert(response.data);
        });
    },
},
mounted:function(){
    this.refreshUserData();
    this.refreshChatData();
}

}