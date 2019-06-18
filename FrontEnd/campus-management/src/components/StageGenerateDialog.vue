<template>
  <v-dialog v-model="show" max-width="400">
    <v-card>
      <v-card-title class="headline">{{ "GENEREAZA TUR" }}</v-card-title>

      <v-card-text>
        <v-layout row wrap>
          <v-flex xs12 lg12>
            <div
              class="subheading"
            >{{"Setati intervalul calendaristic in care s-au efectuat cereri."}}</div>
          </v-flex>
          <v-flex xs12 lg12>
            <v-menu
              v-model="startDateMenu"
              :close-on-content-click="false"
              :nudge-right="30"
              lazy
              transition="scale-transition"
              offset-y
              full-width
              max-width="290px"
              min-width="290px"
            >
              <template v-slot:activator="{ on }">
                <v-text-field
                  v-model="stage.startDate"
                  label="Data de început"
                  hint="Format YYYY-MM-DD"
                  persistent-hint
                  prepend-icon="event"
                  v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker v-model="stage.startDate" no-title @input="startDateMenu = false"></v-date-picker>
            </v-menu>
          </v-flex>
          <v-flex xs12 lg12>
            <v-menu
              v-model="endDateMenu"
              :close-on-content-click="false"
              :nudge-right="30"
              lazy
              transition="scale-transition"
              offset-y
              full-width
              max-width="290px"
              min-width="290px"
            >
              <template v-slot:activator="{ on }">
                <v-text-field
                  v-model="stage.endDate"
                  label="Data de final"
                  hint="Format YYYY-MM-DD"
                  persistent-hint
                  prepend-icon="event"
                  v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker v-model="stage.endDate" no-title @input="endDateMenu = false"></v-date-picker>
            </v-menu>
          </v-flex>
        </v-layout>
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn color="grey darken-3" flat="flat" @click=" addStage(false)">Anuleaza</v-btn>

        <v-btn color="blue darken-1" flat="flat" @click="addStage(true)">Confirmă</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    value: Boolean
  },
  data() {
    0;
    return {
      startDateMenu: false,
      endDateMenu: false,
      stage: {
        startDate: "",
        endDate: "",
        description: ""
      }
    };
  },
  computed: {
    show: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      }
    }
  },
  methods: {
    addStage(decision) {
      if (decision) {
        this.$store
          .dispatch("addStage", this.stage)
          .then(response => {
            this.$store.dispatch("showSnackbar", {
              text: "Turul a fost adaugat cu succes!",
              color: "success"
            });
            this.$store.dispatch("getStage");
            console.log(response);
          })
          .catch(error => {
            this.$store.dispatch("showSnackbar", {
              text: "Hopa! Turul nu a putut fi aduagat!",
              color: "error"
            });
            console.log(error);
          });
      }
      this.show = false;
    }
  }
};
</script>

<style>
</style>
