<template>
  <q-layout view="lHh Lpr lFf">
    <q-page-container>
      <q-page class="row items-center justify-evenly">
        <q-btn
          v-if="!isNewGameStarted && !isGameOver"
          color="primary"
          @click="startNewGame"
        >
          Новая игра
        </q-btn>
        <q-btn
          v-if="isNewGameStarted && !isGameOver"
          icon="keyboard_arrow_up"
          color="primary"
          @click="changeDirection(1)"
        />
        <q-btn
          v-if="isNewGameStarted && !isGameOver"
          icon="keyboard_arrow_down"
          color="primary"
          @click="changeDirection(2)"
        />
        <q-btn
          v-if="isNewGameStarted && !isGameOver"
          icon="keyboard_arrow_left"
          color="primary"
          @click="changeDirection(3)"
        />
        <q-btn
          v-if="isNewGameStarted && !isGameOver"
          icon="keyboard_arrow_right"
          color="primary"
          @click="changeDirection(4)"
        />

        <div style="flex-flow: column">
          <div v-for="(y, idx) in map" :key="idx" class="row">
            <span v-for="(x, idx) in y" :key="idx" class="q">
              {{ getChar(x) }}
            </span>
          </div>
        </div>
      </q-page>
    </q-page-container>
  </q-layout>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { api } from './boot/axios';

interface ICell {
  type: number;
  number: number;
  x: number;
  y: number;
}

const isNewGameStarted = ref(false);
const isGameOver = ref(false);
const mapSize = 44;

const map = ref([] as ICell[][]);

function changeDirection(direction: number) {
  api.get('set_direction?direction=' + direction);
}

function startNewGame() {
  api.get('new_game?mapSize=' + mapSize);
  isNewGameStarted.value = true;
}

function getChar(cell: ICell) {
  if (cell.type == 3) return '@';
  if (cell.type == 4) return '=';
  if (cell.type == 2) return cell.number;
}

void init();
function init() {
  map.value = new Array(mapSize);
  for (let x = 0; x < mapSize; x++) {
    map.value[x] = new Array(mapSize);
    for (let y = 0; y < mapSize; y++) {
      map.value[x][y] = { type: 1, number: 0, x, y };
    }
  }

  setInterval(() => {
    if (isNewGameStarted.value) {
      api.get<boolean>('is_gameover').then((r) => {
        isGameOver.value = r.data;
      });

      api.get<ICell[]>('get_map').then((r) => {
        for (let x = 0; x < mapSize; x++) {
          for (let y = 0; y < mapSize; y++) {
            map.value[y][x].type = 0;
          }
        }

        r.data.forEach((v) => {
          map.value[v.y][v.x].number = v.number;
          map.value[v.y][v.x].type = v.type;
        });
      });
    }
  }, 100);
}
</script>
