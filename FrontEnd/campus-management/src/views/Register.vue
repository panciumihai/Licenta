<template>
  <div class="adaugare-locuri">
    <v-container>
      <v-card>
        <v-card-title class="title">Alegeti caminele disponibile:</v-card-title>
        <v-card-text>
          <v-layout align-space-around justify-start column>
            <v-flex>
              <v-select
                v-model="camineAlese"
                :items="camineTotale"
                item-value="nume"
                item-text="nume"
                attach
                chips
                label="Lista camine"
                multiple
                @change="adaugaCamin"
              ></v-select>
            </v-flex>
            <v-flex class="mt-3" v-show="camine.length > 0">
              <div class="title">Introduceti numarul de locuri pentru caminele selectate:</div>
              <v-layout justify-start wrap>
                <v-flex xs6 sm4 md2 v-for="(camin, index) in camine" :key="camin.nume">
                  <v-text-field
                    class="mx-3"
                    :label="camin.nume"
                    v-model="camin.disponibile"
                    mask="####"
                    @keyup="calculeazaLocuriCamin(index)"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            <v-flex md12 class="mt-3" v-show="camine.length > 0">
              <div class="title">Ajustati procentul de locuri pentru rezerva:</div>
              <v-layout row align-center>
                <div class="title pb-4 mr-4 ml-2">0%</div>
                <v-slider
                  class="my-5"
                  v-model="slider"
                  @change="calculeazaLocuri"
                  thumb-label="always"
                ></v-slider>
                <div class="title pb-4 mr-2 ml-4">100%</div>
              </v-layout>
            </v-flex>
            <v-flex v-show="camine.length > 0">
              <div class="title mb-3 mt-2">Locurile pastrate pentru rezerva:</div>
              <v-layout justify-start wrap>
                <v-flex xs6 sm4 md2 v-for="(camin, index) in camine" :key="camin.nume">
                  <v-text-field
                    class="mx-3"
                    :label="camin.nume"
                    v-model="camin.rezerve"
                    mask="####"
                    v-on:keyup="editareRezerva(index)"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            <v-flex v-show="camine.length > 0">
              <div class="title mb-3 mt-2">Locurile finale disponbile:</div>
              <v-layout justify-start wrap>
                <v-flex xs6 sm4 md2 v-for="(camin, index) in camine" :key="camin.nume">
                  <v-text-field
                    class="mx-3"
                    :label="camin.nume"
                    v-model="camin.alocate"
                    mask="####"
                    v-on:keyup="editareAlocate(index)"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            <v-flex class="mt-3" v-show="camine.length > 0">
              <v-layout justify-center>
                <v-btn dark color="blue">Salveaza locuri</v-btn>
              </v-layout>
            </v-flex>
          </v-layout>
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>

<script>
export default {
  data() {
    return {
      camineAlese: [],
      camineTotale: [
        { nume: "Akademos", disponibile: null, alocate: null, rezerve: null },
        { nume: "Gaudeamus", disponibile: null, alocate: null, rezerve: null },
        { nume: "C1", disponibile: null, alocate: null, rezerve: null },
        { nume: "C2", disponibile: null, alocate: null, rezerve: null },
        { nume: "C3", disponibile: null, alocate: null, rezerve: null },
        { nume: "C4", disponibile: null, alocate: null, rezerve: null },
        { nume: "C5", disponibile: null, alocate: null, rezerve: null },
        { nume: "C6", disponibile: null, alocate: null, rezerve: null },
        { nume: "C7", disponibile: null, alocate: null, rezerve: null },
        { nume: "C8", disponibile: null, alocate: null, rezerve: null },
        { nume: "C9", disponibile: null, alocate: null, rezerve: null },
        { nume: "C10", disponibile: null, alocate: null, rezerve: null },
        { nume: "C11", disponibile: null, alocate: null, rezerve: null },
        { nume: "C12", disponibile: null, alocate: null, rezerve: null }
      ],
      camine: [],
      slider: 10
    };
  },
  methods: {
    calculeazaLocuri() {
      for (let i = 0; i < this.camine.length; i++) {
        if (this.camine[i].disponibile != null) {
          this.camine[i].alocate = Math.ceil(
            ((100 - this.slider) / 100) * this.camine[i].disponibile
          );
          this.camine[i].rezerve =
            0 + this.camine[i].disponibile - this.camine[i].alocate;
        }
      }
    },
    adaugaCamin() {
      this.camine = this.camineTotale.filter(c => {
        return (
          this.camineAlese.find(n => {
            return n == c.nume;
          }) != undefined
        );
      });
    },

    calculeazaLocuriCamin(index) {
      this.camine[index].alocate = Math.ceil(
        ((100 - this.slider) / 100) * this.camine[index].disponibile
      );
      this.camine[index].rezerve =
        0 + this.camine[index].disponibile - this.camine[index].alocate;
    },
    editareRezerva(index) {
      if (
        Number(this.camine[index].rezerve) >
        Number(this.camine[index].disponibile)
      )
        this.camine[index].rezerve = this.camine[index].disponibile;

      this.camine[index].alocate =
        this.camine[index].disponibile - this.camine[index].rezerve;
    },
    editareAlocate(index) {
      if (
        Number(this.camine[index].alocate) >
        Number(this.camine[index].disponibile)
      )
        this.camine[index].alocate = this.camine[index].disponibile;

      this.camine[index].rezerve =
        this.camine[index].disponibile - this.camine[index].alocate;
    }
  },
  computed: {}
};
</script>

<style>
</style>
