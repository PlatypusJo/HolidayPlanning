import cl from './AdviceContainer.module.css'
import {BulbOutlined} from "@ant-design/icons";

export const AdviceContainer = () => {
    return (
        <div className={cl.adviceContainer}>
            <div className={cl.textMenu}>
                <div className={cl.menuBlok}>Советы</div>
                <div className={cl.menuUnderline}></div>
                <div className={cl.adviceText}>
                    <BulbOutlined className={cl.adviceIcon}/>
                    Прежде чем начинать планировать, определите, сколько вы можете потратить на праздник. Это поможет вам сузить выбор и избежать ненужных трат.
                </div>
                <div className={cl.adviceText}>
                    <BulbOutlined className={cl.adviceIcon}/>
                    Разбейте планирование на этапы, чтобы не упустить важные детали. Например, составьте список гостей, меню, необходимых покупок и украшений.
                </div>
                <div className={cl.adviceText}>
                    <BulbOutlined className={cl.adviceIcon}/>
                    Спросите своих гостей, что им нравится, какие у них есть диетические ограничения или аллергии. Это поможет вам создать меню, которое понравится всем.
                </div>
                <div className={cl.adviceText}>
                    <BulbOutlined className={cl.adviceIcon}/>
                    Планы могут меняться, поэтому важно быть готовым к неожиданностям. Не бойтесь импровизировать и не переживайте о мелких недочетах.
                </div>
            </div>
        </div>
    );
};