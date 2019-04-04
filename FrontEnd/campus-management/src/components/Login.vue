<template>
  <v-dialog max-width="400px" v-model="show">
    <v-card>
      <v-toolbar dark color="blue">
        <v-toolbar-title>Autentificare</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn icon @click="show = false">
          <v-icon large>close</v-icon>
        </v-btn>
      </v-toolbar>

      <v-card-text>
        <v-form class="px-3" ref="form">
          <v-text-field
            prepend-icon="person"
            label="Email"
            v-model="email"
            name="input-10-1"
            :rules="[rules.required, rules.emailMatch]"
            counter
          ></v-text-field>
          <v-text-field
            prepend-icon="vpn_key"
            label="Parola"
            v-model="password"
            :append-icon="passShow ? 'visibility' : 'visibility_off'"
            :type="passShow ? 'text' : 'password'"
            hint="Foloseste litere mari, mici si simboluri"
            :rules="[rules.required, rules.min6]"
            name="input-10-1"
            counter
            @click:append="passShow = !passShow"
          ></v-text-field>

          <v-spacer></v-spacer>
          <v-container fluid>
            <v-layout row align-end justify-space-between r>
              <v-flex>
                <v-btn depressed dark color="blue" @click="submit">Autentificare</v-btn>
              </v-flex>
              <v-flex>
                <router-link to="/">
                  <p>Am uitat parola :(</p>
                </router-link>
              </v-flex>
            </v-layout>
          </v-container>
        </v-form>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    value: Boolean
  },
  data() {
    return {
      passShow: false,
      password: "",
      email: "",
      rules: {
        required: v => !!v || "Camp obligatoriu",
        min8: v => v.length >= 8 || "Minim 8 caractere",
        min6: v => v.length >= 3 || "Minim 6 caractere",
        emailMatch: v =>
          /^[a-z0-9]+@[a-z0-9]+\.[a-z0-9]+/.test(v) || "Emailul nu este valid"
      }
    };
  },
  methods: {
    submit() {
      if (this.$refs.form.validate()) {
        // eslint-disable-next-line
        console.log("e bun");
      }
    },
    reset() {
      //this.$refs.form.reset();
      this.email = "";
      this.password = "";
      this.$refs.form.resetValidation();
    }
  },
  computed: {
    show: {
      get() {
        return this.value;
      },
      set(value) {
        this.reset();
        this.$emit("input", value);
      }
    }
  }
};
</script>

<style>
</style>
