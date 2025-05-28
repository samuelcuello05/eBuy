import Styles from './Leftbar.module.css';

export default function Leftbar({rol}){
    //Modify this component to render different content based on the role
    //When the API is published
    if(rol==="employee"){
        return(
            <aside className={Styles["leftbar"]}>
            
            </aside>
        );
    }
    return(
        <aside className={Styles["leftbar"]}>
            
        </aside>
    );
}