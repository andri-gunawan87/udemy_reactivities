import { useLocalObservable } from "mobx-react-lite"
import {HubConnection, HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr"
import { useEffect, useRef } from "react";
import { runInAction } from "mobx";

export const useComments = (acticityId?: string) => {
    const created = useRef(false)
    const commentStore = useLocalObservable(() => ({
        comments: [] as ChatComment[],
        hubConnection: null as HubConnection | null,

        createHubConnection(acticityId: string) {
            if (!acticityId) return;

            this.hubConnection = new HubConnectionBuilder()
                .withUrl(`${import.meta.env.VITE_COMMENTS_URL}?activityId=${acticityId}`, {
                    withCredentials: true
                })
                .withAutomaticReconnect()
                .build();

            this.hubConnection.start().catch(error => 
                console.log("Error establishing connection: ", error))
            
            this.hubConnection.on("LoadComments", comments => {
                runInAction(() => {
                    this.comments = comments
                })
            })

            this.hubConnection.on("ReceiveComment", comment => {
                runInAction(() => {
                    this.comments.unshift(comment);
                })
            })
        },
        
        stopHubConnection() {
            if (this.hubConnection?.state === HubConnectionState.Connected) {
                this.hubConnection.stop().catch(error => 
                console.log("Error stopping connection: ", error))
            }
        }
    }));

    useEffect(() => {
        if (acticityId && !created.current) {
            commentStore.createHubConnection(acticityId)
            created.current = true;
        }

        return () => {
            commentStore.stopHubConnection();
            commentStore.comments = [];
        }
    }, [acticityId, commentStore])

    return {
        commentStore
    }
}