<template>
  <v-dialog v-model="show" persistent max-width="600px">
    <v-card>
      <v-card-title>
        <span class="headline">Date student</span>
      </v-card-title>
      <v-card-text>
        <v-container grid-list-md>
          <v-form class="px-3" ref="form">
            <v-layout wrap>
              <v-flex xs12 sm6 md4>
                <v-text-field label="Nume" required v-model="student.lastName" :rules="rules"></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md4>
                <v-text-field label="Prenume" required v-model="student.firstName" :rules="rules"></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md4>
                <v-text-field
                  label="Nationalitate"
                  required
                  v-model="student.nationality"
                  :rules="rules"
                ></v-text-field>
              </v-flex>
              <v-flex xs12 md12>
                <v-text-field label="Email" required v-model="student.email" :rules="rules"></v-text-field>
              </v-flex>
              <v-flex xs12 md12>
                <v-text-field label="CNP" required v-model="student.cnp" :rules="rules"></v-text-field>
              </v-flex>
              <v-flex xs12 md6>
                <v-autocomplete
                  :items="['M','F']"
                  label="Sex"
                  required
                  v-model="student.gender"
                  :rules="rules"
                ></v-autocomplete>
              </v-flex>
              <v-flex xs12 sm6>
                <v-select
                  :items="['1', '2', '3', '4', '5']"
                  label="An de studiu"
                  required
                  v-model="student.year"
                  :rules="rules"
                ></v-select>
              </v-flex>
              <v-flex xs12 sm6 md6>
                <v-text-field label="Punctaj 1" required v-model="student.score" :rules="rules"></v-text-field>
              </v-flex>
              <v-flex xs12 sm6 md6>
                <v-text-field
                  label="Punctaj 2"
                  required
                  v-model="student.secondScore"
                  :rules="rules"
                ></v-text-field>
              </v-flex>
            </v-layout>
          </v-form>
        </v-container>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" flat @click="show = false">Close</v-btn>
        <v-btn color="blue darken-1" flat @click="addStudent()">Save</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    value: Boolean,
    updateStudent: {}
  },
  data() {
    return {
      defaultStudent: {
        year: null,
        cnp: "",
        nationality: "",
        score: null,
        secondScore: null,
        firstName: "",
        lastName: "",
        email: "",
        gender: null,
        password: "parola123"
      },
      student: {
        year: null,
        cnp: "",
        nationality: "",
        score: null,
        secondScore: null,
        firstName: "",
        lastName: "",
        email: "",
        gender: null,
        password: "parola123"
      },
      rules: [v => !!v || "Camp obligatoriu"]
    };
  },
  methods: {
    addStudent() {
      if (this.$refs.form.validate()) {
        this.$store
          .dispatch("addStudent", this.student)
          .then(response => {
            this.$store.dispatch("showSnackbar", {
              text: "Studentul a fost adaugat cu succes!",
              color: "success"
            });
            this.$store.dispatch("getStudents");
            console.log(response);
          })
          .catch(error => {
            this.$store.dispatch("showSnackbar", {
              text: "Hopa! Studentul nu a putut fi adaugat!",
              color: "error"
            });
            console.log(error);
          });
        this.show = false;
      }
    }
  },
  computed: {
    show: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
        this.student = this.defaultStudent;
        this.$refs.form.resetValidation();
      }
    }
  }
};
</script>

<style>
</style>
