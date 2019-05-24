<template>
  <div class="accommodation-request">
    <v-container>
      <v-card>
        <v-form>
          <v-card-title class="display-1">{{ title }}</v-card-title>
          <v-card-text>
            <v-layout column wrap>
              <v-flex>
                <div class="title mt-3">Fiu / Fiica de cadru didactic?</div>
                <v-layout>
                  <v-radio-group v-model="application.childOfTeacher" row>
                    <v-radio color="blue" label="Nu" value="false"></v-radio>
                    <v-radio color="blue" label="Da" value="true"></v-radio>
                  </v-radio-group>
                </v-layout>
              </v-flex>
              <v-flex>
                <div class="title mt-3">Cazuri speciale? (Social / Medical / Orfan / Altele)</div>
                <v-layout>
                  <v-checkbox
                    color="blue"
                    v-model="specialCasesArray"
                    label="Social"
                    value="Social"
                  ></v-checkbox>
                  <v-checkbox
                    color="blue"
                    v-model="specialCasesArray"
                    label="Medical"
                    value="Medical"
                  ></v-checkbox>
                  <v-checkbox color="blue" v-model="specialCasesArray" label="Orfan" value="Orfan"></v-checkbox>
                  <v-checkbox
                    color="blue"
                    v-model="specialCasesArray"
                    label="Altele"
                    value="Altele"
                  ></v-checkbox>
                </v-layout>
              </v-flex>
              <v-flex>
                <div class="title mt-3">Bursier al statului roman?</div>
                <v-layout>
                  <v-radio-group v-model="application.scholarship" row>
                    <v-radio color="blue" label="Nu" value="false"></v-radio>
                    <v-radio color="blue" label="Da" value="true"></v-radio>
                  </v-radio-group>
                </v-layout>
              </v-flex>
              <v-flex>
                <div class="title mt-3">Daca solicita cazare sau nu?</div>
                <v-layout>
                  <v-radio-group v-model="application.accommodationRequest" row>
                    <v-radio color="blue" label="Nu" value="false"></v-radio>
                    <v-radio color="blue" label="Da" value="true"></v-radio>
                  </v-radio-group>
                </v-layout>
              </v-flex>
              <v-flex>
                <div
                  class="title mt-3"
                >Unde solicitati cazare? (Comisia va acorda cazare dupa ordinea optiunilor studentului)</div>
                <v-layout>
                  <v-select
                    menu-props="offsetY"
                    v-model="hostelIds"
                    :items="hostels"
                    item-value="id"
                    item-text="name"
                    attach
                    chips
                    label="Lista preferinte"
                    multiple
                  ></v-select>
                </v-layout>
              </v-flex>
              <v-flex>
                <div class="title mt-3">Unde ai locuit in anul precedent?</div>
                <v-layout>
                  <v-text-field label="Locuinta" v-model="application.lastYearLocation"></v-text-field>
                </v-layout>
              </v-flex>
            </v-layout>
            <v-flex>
              <v-layout justify-center>
                <v-btn class="mt-3" dark color="blue" @click="submit">Trimite Cererea</v-btn>
              </v-layout>
            </v-flex>
          </v-card-text>
        </v-form>
      </v-card>
    </v-container>
  </div>
</template>

<script>
export default {
  data() {
    return {
      title: "Formular pentru solicitare de cazare",
      hostelIds: [],
      specialCasesArray: [],
      application: {
        childOfTeacher: "false",
        specialCases: "",
        scholarship: "false",
        accommodationRequest: "true",
        lastYearLocation: "",
        hostelPreferences: []
      },
      listaPreferinteCamine: [
        { nume: "Akademos" },
        { nume: "Gaudeamus" },
        { nume: "C1" },
        { nume: "C2" },
        { nume: "C3" },
        { nume: "C4" },
        { nume: "C5" },
        { nume: "C6" },
        { nume: "C7" },
        { nume: "C8" },
        { nume: "C9" },
        { nume: "C10" },
        { nume: "C11" },
        { nume: "C12" }
      ]
    };
  },
  methods: {
    submit() {
      this.application.hostelPreferences = [];
      this.hostelIds.forEach((element, index) => {
        let hostel = {};
        hostel.hostelId = element;
        hostel.preferenceNumber = index + 1;
        this.application.hostelPreferences.push(hostel);
      });
      this.application.studentId = this.student.id;
      this.application.specialCases = "";
      this.specialCasesArray.forEach(
        element => (this.application.specialCases += ", " + element)
      );
      this.application.specialCases = this.application.specialCases.substr(2);
      //console.log(this.application.hostelPreferences);
      this.$store.dispatch("addApplication", this.application);
    }
  },
  computed: {
    selectedHostels() {
      return {};
    },
    hostels() {
      return this.$store.getters.hostels;
    },
    student() {
      return this.$store.getters.student;
    }
  }
};
</script>

<style>
</style>
