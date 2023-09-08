import { IBaseEntity } from "./IBaseEntity";
import { ITagColor } from "./ITagColor";

export interface ITag extends IBaseEntity {
    tagLabel: string,
    tagColor: ITagColor | null,
    //appUserId: string
}