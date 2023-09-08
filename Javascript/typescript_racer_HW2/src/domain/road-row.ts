export class RoadRow {
    leftEdge: number;
    rightEdge: number;
    obstacle: number | null;
    static ROAD_MINIMUM_WIDTH: number;
    constructor(leftEdge : number, obstacle : number |null) {
        this.leftEdge = leftEdge;
        this.rightEdge = leftEdge + RoadRow.ROAD_MINIMUM_WIDTH;
        this.obstacle = obstacle;

        if (this.obstacle) {
            if (this.obstacle <= leftEdge || this.obstacle >= this.rightEdge) {
                this.obstacle = null;
            }
        }

    }
}

RoadRow.ROAD_MINIMUM_WIDTH = 0.4;