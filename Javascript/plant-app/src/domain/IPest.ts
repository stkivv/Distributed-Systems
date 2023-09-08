import { IBaseEntity } from "./IBaseEntity";
import { IPestSeverity } from "./IPestSeverity";
import { IPestType } from "./IPestType";

export interface IPest extends IBaseEntity {
    pestComment: string,
    pestDiscoveryTime: Date,
    plantId: string,
    pestType: IPestType | null,
    pestSeverity: IPestSeverity | null
}
