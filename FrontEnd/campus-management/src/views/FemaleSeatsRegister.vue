<template>
  <div class="adaugare-locuri">
    <v-container>
      <v-card>
        <v-card-title class="title">Alegeti caminele disponibile pentru FETE:</v-card-title>
        <v-card-text>
          <v-layout align-space-around justify-start column>
            <v-flex>
              <v-select
                v-model="hostelsStatus"
                :items="hostels"
                item-value="hostelId"
                item-text="name"
                attach
                chips
                label="Lista camine"
                multiple
                menu-props="offsetY"
                return-object
              ></v-select>
            </v-flex>
            <v-flex class="mt-3" v-show="hostelsStatus.length > 0">
              <div class="title">Introduceti numarul de locuri pentru caminele selectate:</div>
              <v-layout justify-start wrap>
                <v-flex xs6 sm4 md2 v-for="(hostel) in hostelsStatus" :key="hostel.id">
                  <v-text-field
                    class="mx-3"
                    :label="hostel.name"
                    v-model="hostel.totalFemaleSeats"
                    mask="####"
                    @keyup="computeHostelSeats(hostel)"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            <v-flex md12 class="mt-3" v-show="hostelsStatus.length > 0">
              <div class="title">Ajustati procentul de locuri pentru rezerva:</div>
              <v-layout row align-center>
                <div class="title pb-4 mr-4 ml-2">0%</div>
                <v-slider
                  class="my-5"
                  v-model="slider"
                  thumb-label="always"
                  @change="sliderChanged"
                ></v-slider>
                <div class="title pb-4 mr-2 ml-4">100%</div>
              </v-layout>
            </v-flex>
            <v-flex v-show="hostelsStatus.length > 0">
              <div class="title mb-3 mt-2">Locurile pastrate pentru rezerva:</div>
              <v-layout justify-start wrap>
                <v-flex xs6 sm4 md2 v-for="(hostel) in hostelsStatus" :key="hostel.hostelId">
                  <v-text-field
                    class="mx-3"
                    :label="hostel.name"
                    v-model="hostel.reservedFemaleSeats"
                    mask="####"
                    v-on:keyup="reservedFemaleSeatsChanged(hostel)"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            <v-flex v-show="hostelsStatus.length > 0">
              <div class="title mb-3 mt-2">Locurile finale disponbile:</div>
              <v-layout justify-start wrap>
                <v-flex xs6 sm4 md2 v-for="(hostel) in hostelsStatus" :key="hostel.hostelId">
                  <v-text-field
                    class="mx-3"
                    :label="hostel.name"
                    v-model="hostel.femaleSeats"
                    mask="####"
                    v-on:keyup="femaleSeatsChanged(hostel)"
                  ></v-text-field>
                </v-flex>
              </v-layout>
            </v-flex>
            <v-flex class="mt-3" v-show="hostelsStatus.length > 0">
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
      selectedHostels: [],
      hostelsStatus: [],
      slider: 10
    };
  },
  methods: {
    addHostelStatus() {
      this.hostelsStatus.forEach(hostel => {
        if ("totalFemaleSeats" in hostel) {
          (hostel.totalFemaleSeats = null),
            (hostel.femaleSeats = null),
            (hostel.reservedFemaleSeats = null);
        }
      });
    },

    computeHostelSeats(hostel) {
      hostel.femaleSeats = Math.ceil(
        ((100 - this.slider) / 100) * hostel.totalFemaleSeats
      );
      hostel.reservedFemaleSeats =
        0 + hostel.totalFemaleSeats - hostel.femaleSeats;
    },
    sliderChanged() {
      this.hostelsStatus.forEach(h => {
        if (h.totalFemaleSeats != null) {
          h.femaleSeats = Math.ceil(
            ((100 - this.slider) / 100) * h.totalFemaleSeats
          );
          h.reservedFemaleSeats = 0 + h.totalFemaleSeats - h.femaleSeats;
        }
      });
    },
    reservedFemaleSeatsChanged(hostel) {
      if (Number(hostel.reservedFemaleSeats) > Number(hostel.totalFemaleSeats))
        hostel.reservedFemaleSeats = hostel.totalFemaleSeats;

      hostel.femaleSeats = hostel.totalFemaleSeats - hostel.reservedFemaleSeats;
    },
    femaleSeatsChanged(hostel) {
      if (Number(hostel.femaleSeats) > Number(hostel.totalFemaleSeats))
        hostel.femaleSeats = hostel.totalFemaleSeats;

      hostel.reservedFemaleSeats = hostel.totalFemaleSeats - hostel.femaleSeats;
    }
  },
  computed: {
    hostels() {
      return this.$store.getters.hostels.map(function(h) {
        return {
          hostelId: h.id,
          name: h.name,
          totalFemaleSeats: null,
          femaleSeats: null,
          reservedFemaleSeats: null
        };
      });
    }
  }
};
</script>

<style>
</style>
