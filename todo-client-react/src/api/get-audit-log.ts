import axios from "axios";

export type GetAuditLogResponse = {
    auditLog: AuditEntry[];
}

export type AuditEntry = {
    auditEntryId: string;
    auditDate: string;
    action: string;
    todoId: string;
    auditInfo: string;
}

export const getAuditLog = async () => {
    const { data } = await axios.get<GetAuditLogResponse>("api/todos/audit-log");
    return data;
}
