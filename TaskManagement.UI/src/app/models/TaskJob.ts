import { StatusEnum } from "../enums/StatusEnum";

export interface TaskJob {
    id: string;
    title: string;
    description: string;
    statusEnum: StatusEnum;
    creationDate: Date;
    updatedData: Date;
    isDeleted: boolean;
    userId: string;
}