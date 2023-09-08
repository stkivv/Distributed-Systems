import { RoadRow } from "../domain/road-row";

export class Game {
    state: { map: RoadRow[]; is_running: boolean; speed: number; pastscores: number[]; currentscore: number; };
    position: number;
    directionLeft: boolean;
    directionCounterDefault: number;
    directionCounter: number;
    objectLocation: number | null;
    static MAP_ROWS: number = 25;
    constructor() {
        this.state = {
            map: [],
            is_running: false,
            speed: 100,
            pastscores: [],
            currentscore: 0
        }
        this.position = 0.325;
        this.directionLeft = true;
        this.directionCounterDefault = 7;
        this.directionCounter = this.directionCounterDefault;
        this.initializeState();
        this.objectLocation = null;
    }

    addNewMapRow(obstaclesFlag : boolean){
        console.log("making row...")
        let randBool = Math.random();

        if (randBool < 0.5 && obstaclesFlag){ //change this condition to make obj spawns more/less probable
            this.objectLocation = Math.random() * (0.7 - 0.3) + 0.3;
        } else {
            this.objectLocation = null;
        }

        //console.log(this.objectLocation)
        this.state.map.push(
            new RoadRow(this.position, this.objectLocation)
        )
        if (this.position > 0.2 && this.directionLeft) {
            this.position = this.position - 0.005;
        }
        if (this.position + RoadRow.ROAD_MINIMUM_WIDTH < 0.8 && !this.directionLeft) {
            this.position = this.position + 0.005;
        }

        let randBool2 = Math.random();
        if (this.directionCounter == 0) {
            if (randBool2 < 0.5){
                this.directionLeft = true;
            } else{
                this.directionLeft = false;
            }
            this.directionCounter = this.directionCounterDefault;
        } else {
            this.directionCounter--;
        }

        if (this.state.map.length > Game.MAP_ROWS){
            this.state.map.splice(0, 1)
        }
    }

    initializeState() {
        for (let index = 0; index < Game.MAP_ROWS; index++) {
            this.addNewMapRow(false);
        }
    }
}

