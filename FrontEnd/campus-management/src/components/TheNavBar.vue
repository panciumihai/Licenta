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

        <v-btn
          v-show="person.id != null"
          flat
          color="blue"
          class="ml-3 mr-0"
          @click="drawer=false"
          to="/logout"
        >
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
            <img src="../assets/user.png" alt>
          </v-list-tile-avatar>

          <v-list-tile-content>
            <v-list-tile-title>
              <div>
                {{person.firstName}}
                <div class="text" v-for="role in person.personRoles" :key="role">{{ role +" " }}</div>
              </div>
            </v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>

      <v-list dense>
        <v-divider></v-divider>
        <div v-for="item in drawerItems" :key="item.title">
          <v-list-group
            v-if="item.items != null"
            v-model="item.active"
            :prepend-icon="item.action"
            no-action
          >
            <template v-slot:activator>
              <v-list-tile>
                <v-list-tile-content>{{ item.title }}</v-list-tile-content>
              </v-list-tile>
            </template>

            <v-list-tile
              active-class="blue lighten-3 black--text"
              v-for="subItem in item.items"
              :key="subItem.title"
              :to="{ name: subItem.route }"
            >
              <v-list-tile-content>
                <v-list-tile-title>{{ subItem.title }}</v-list-tile-title>
              </v-list-tile-content>

              <v-list-tile-action>
                <v-icon>{{ subItem.action }}</v-icon>
              </v-list-tile-action>
            </v-list-tile>
          </v-list-group>

          <v-list-tile v-else active-class="blue lighten-3 black--text" :to="{ name: item.route }">
            <v-list-tile-action>
              <v-icon>{{ item.action }}</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>{{ item.title }}</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </div>
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
        { title: "Articole", icon: "keyboard_arrow_right", route: "/articles" },
        { title: "Repartizari", icon: "keyboard_arrow_right", route: "/utile" },
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
    drawerItems() {
      let drawerItems = [];
      if (this.isAdmin)
        drawerItems = [
          {
            title: "Adauga articol",
            action: "list_alt",
            route: "articleCreator"
          },
          {
            title: "Cereri de cazare",
            action: "face",
            route: "applications"
          },
          {
            title: "Distributie studenti",
            active: false,
            action: "accessibility_new",
            items: [
              { title: "Distributie fete", route: "femaleDistribution" },
              { title: "Distributie baieti", route: "maleDistribution" }
            ]
          },
          {
            title: "Adauga locuri camine",
            active: false,
            action: "note_add",
            items: [
              { title: "Locuri fete", route: "femaleSeatsRegister" },
              { title: "Locuri baieti", route: "maleSeatsRegister" }
            ]
          },
          {
            title: "Repartizeaza locuri",
            action: "domain",
            route: "hostelsStatusPreview"
          },
          {
            title: "Repartizare studenti",
            action: "local_hotel",
            route: "allocationsPreview"
          }
        ];
      else if (this.isStudent)
        drawerItems = [
          {
            title: "Cerere cazare",
            action: "domain",
            route: "accommodationRequest"
          }
        ];
      return drawerItems;
    },

    setTab() {
      var i;
      for (i = 0; i < this.navBarItems.length; ++i) {
        if (this.$route.path == this.navBarItems[i].route)
          return this.$route.path;
      }
      return "/nan";
    },
    person() {
      return this.$store.getters.person;
    },
    isStudent() {
      return this.$store.getters.isStudent;
    },
    isAdmin() {
      return this.$store.getters.isAdmin;
    }
  }
};
</script>

<style scoped>
</style>
