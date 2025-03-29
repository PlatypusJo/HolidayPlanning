import {GiftOutlined, ShopOutlined, SolutionOutlined, TeamOutlined} from "@ant-design/icons";
import cl from "./ProfileMenu.module.css"
import {useNavigate} from "react-router-dom";
import {RoutesPaths} from "../../config";

export const ProfileMenu = () => {
    const navigate = useNavigate()

    return (
        <div className={cl.profileMenu}>
            <div className={cl.textMenu}>
                <div className={cl.menuBlock}>Меню</div>
                <div className={cl.menuUnderline}></div>
                <div className={cl.container}>
                    <div className={cl.tabMenu}>
                        <GiftOutlined className={cl.tabMenuIcon}/>
                        <div className={cl.tabMenuText} onClick={()=> navigate(RoutesPaths.EVENTS)}>Мероприятия</div>
                    </div>
                    <div className={cl.tabMenu}>
                        <SolutionOutlined className={cl.tabMenuIcon}/>
                        <div className={cl.tabMenuText} onClick={() => navigate(`${RoutesPaths.EVENTS_CONTRACTORS}`.replace(":id", `${localStorage.getItem('selectEventId')}`))}>Подрядчики</div>
                    </div>
                    <div className={cl.tabMenu}>
                        <TeamOutlined className={cl.tabMenuIcon}/>
                        <div className={cl.tabMenuText} onClick={() => navigate(`${RoutesPaths.EVENTS_GUESTS}`.replace(":id", `${localStorage.getItem('selectEventId')}`))}>Гости</div>
                    </div>
                </div>
            </div>
        </div>
    );
};