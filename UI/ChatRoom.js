const ChatRoom={template:`
  <div id="app">
    <div class="chat-container">
      <!-- User List on the Left -->
      <div class="participants">
        <div v-for="user in ChatUsers" :key="user.UserId">
          <p>{{ user.UserLogin }}</p>
        </div>
      </div>

      <!-- Messages on the right -->
      <div class="Messages">
        <div v-for="message in Messages" :key="message.MessageId">
          <p>{{ message.CreaterId }}: {{ message.MessageBody }}</p>
        </div>
      </div>

      <!-- New messages at the bottom -->
      <div class="Messages-input">
        <input type="text" v-model="newMessage" placeholder="Enter message">
        <button @click="sendMessage">Send</button>
      </div>
    </div>
  </div>
`, style: 
`
.chat-container {
  display: flex;
  flex-direction: row;
}

.participants {
  width: 20%;
  border-right: 1px solid #ccc;
  padding: 10px;
}

.Messages {
  flex-grow: 1;
  padding: 10px;
}

.Messages-input {
  width: 100%;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  background-color: #f0f0f0;
}
`,
data(){
    return {
        ChatUsers:[],
        Users:[],
        OurUser: "",
        OurChat: "",
        Messages: [],
        UsersRoles:[],
        modalTitle: "",
        newMessage: "",
    }
},
methods:{
    sendMessage() {
        axios.post(variables.API_URL + "Message", {
            ChatId: this.OurChat.ChatId,
            CreaterId: this.OurUser.UserId,
            MessageBody: this.newMessage,
        }).then((response) => {
            this.refreshMessageData();
            this.newMessage = '';
        });
    },
    getOurChat(){
        axios.get(variables.API_URL + "Chat")
        .then((response) => {
            this.OurChat = response.data.find((chat) => {
                return chat.ChatName === this.$route.params.chatName;
            });
        })
        .catch((error) => {
            console.error("Error while data loading:", error);
        });
    },
    getAllUsers(){
        axios.get(variables.API_URL+"User")
        .then((response)=>{
            this.Users=response.data;
            this.OurUser = this.Users.find((user) => {
                return user.UserLogin === this.$route.params.userLogin;
            });
        });
    },
    refreshChatUsers(){
        axios.get(variables.API_URL+"ChatUsers", {
            params: {
                chatId: this.OurChat.ChatId
            }
            })
        .then((response)=>{
            response.data.find((chatUser) => {
                if(chatUser.ChatId === this.OurChat.ChatId){
                    this.ChatUsers.push(this.getUserById(chatUser.UserId));
                };
            });
        });
    },
    getUserById(id){
        return this.Users.find((user) => {
            return user.UserId === id
        });
    },
    refreshMessageData(){
        axios.get(variables.API_URL+"Message", {
            params: {
                chatId: this.OurChat.ChatId
            }
            })
        .then((response)=>{
            this.Messages=response.data;
        });
    },

},
mounted: async function () {
    try {
      await this.getOurChat();
      await this.getAllUsers();
      await this.refreshChatUsers();
      await this.refreshMessageData();
    } catch (error) {
    }
  },
}