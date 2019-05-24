<template>
  <nav>
    <v-toolbar clipped-right app class="toolbar">
      <v-toolbar-side-icon v-show="person.id != null" @click="drawer = !drawer" class="blue--text"></v-toolbar-side-icon>

      <router-link to="/" tag="button">
        <v-toolbar-title>
          <v-avatar tile class="mr-1">
            <img src="../assets/Logo_UAIC_Iasi.svg" alt>
          </v-avatar>
          <span class="font-weight-light">Campus</span>
          <span>Management</span>
        </v-toolbar-title>
      </router-link>
      <v-spacer></v-spacer>

      <v-toolbar-items class="hidden-sm-and-down">
        <!-- <v-btn flat color="blue" class="mx-0" v-for="item in navBarItems" :key="item.title">
          <span class="mr-2">{{ item.title }}</span>
        </v-btn>-->

        <v-tabs centered slider-color="blue" height="64px" :value="setTab">
          <v-tab v-show="false" to="/nan"></v-tab>
          <v-tab
            flat
            class="grey lighten-4 blue--text"
            v-for="item in navBarItems"
            :key="item.title"
            :to="item.route"
            router="true"
          >
            <span class="mr-2">{{ item.title }}</span>
          </v-tab>
        </v-tabs>

        <v-btn
          v-show="person.id == null"
          flat
          color="blue"
          class="ml-3 mr-0"
          @click="dialog = true"
        >
          <span class="mr-2">Autentificare</span>
          <v-icon>meeting_room</v-icon>
          <Login/>
        </v-btn>

        <v-btn v-show="person.id != null" flat color="blue" class="ml-3 mr-0" to="/logout">
          <span class="mr-2">Deconectare</span>
          <v-icon>meeting_room</v-icon>
          <Login/>
        </v-btn>
        <!-- 
        <v-btn depressed dark color="blue" :to="`/register`">
          <span class="mr-2">Inscriere</span>
          <v-icon>exit_to_app</v-icon>
        </v-btn>-->
      </v-toolbar-items>
    </v-toolbar>
    <v-navigation-drawer disable-resize-watcher v-model="drawer" app dark class="blue" width="250">
      <v-list class="pa-1">
        <v-list-tile avatar>
          <v-list-tile-avatar>
            <img
              src="https://yt3.ggpht.com/a-/AAuE7mATBPn2Fmw2O1eKHBLGTKT9oCzeEqddNvPxeg=s900-mo-c-c0xffffffff-rj-k-no"
            >
          </v-list-tile-avatar>

          <v-list-tile-content>
            <v-list-tile-title>{{person.firstName}}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>

      <v-list class="pt-0" dense>
        <v-divider></v-divider>

        <v-list-tile
          dark
          v-for="item in navBarItems"
          :key="item.title"
          :to="item.route"
          active-class="blue lighten-3 black--text"
        >
          <v-list-tile-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>

          <v-list-tile-content>
            <v-list-tile-title>{{ item.title }}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <Login v-model="dialog"></Login>
  </nav>
</template>

<script>
import Login from "@/components/Login";

export default {
  components: {
    Login
  },
  data() {
    return {
      drawer: false,
      dialog: false,
      navBarItems: [
        { title: "Cămine", icon: "keyboard_arrow_right", route: "/camine" },
        { title: "Utile", icon: "keyboard_arrow_right", route: "/utile" },
        { title: "Cantină", icon: "keyboard_arrow_right", route: "/cantina" },
        { title: "Contact", icon: "keyboard_arrow_right", route: "/contact" }
      ]
    };
  },
  methods: {
    openLoginDialog: function() {
      // eslint-disable-next-line
      //console.log("click");
      this.$emit("dialog", true);
    }
  },
  computed: {
    setTab: function() {
      var i;
      for (i = 0; i < this.navBarItems.length; ++i) {
        if (this.$route.path == this.navBarItems[i].route)
          return this.$route.path;
      }
      return "/nan";
    },
    person() {
      return this.$store.getters.person;
    }
  }
};
</script>

<style scoped>
</style>
