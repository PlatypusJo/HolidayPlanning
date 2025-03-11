import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {RoutesPaths} from "../../shared/config";
import {LoginOutlined} from "@ant-design/icons";
import {InfoContainer} from "../../shared/ui";
import { ContentBlock } from "../../shared/ui/ContentBlock/ContentBlock";
import {AuthorizationModal} from "../../modal/AutorizationModal";
import ImageContainerProfile from "../../shared/image/image-container-profile.png"
import ImBlock2 from '../../shared/image/image-container-block1.png'
import ImBlock3 from '../../shared/image/image-container-block2.png'
import ImBlock4 from '../../shared/image/image-container-block3.png'
import ImBlock5 from '../../shared/image/image-container-block4.png'
import ImBlock6 from '../../shared/image/image-container-block5.png'

export const HomePage = () => {
    const navigate = useNavigate()
     const [isModalVisible, setIsModalVisible] = useState(false);
    const [isAuth, setIsAuth] = useState(false);

    useEffect(() => {
        localStorage.getItem('userId') && navigate(RoutesPaths.PROFILE)
    }, []);

    const showModal = () => {
        setIsModalVisible(true);
    };
    const handleCancel = () => {
        setIsModalVisible(false);
    };

    return (
        <>
            <InfoContainer
                title={"Ваши мероприятия"}
                src={ImageContainerProfile}
                onBtnClick={showModal}
                buttonSettings={{ btnIcon: <LoginOutlined/>, iconPosition: 'end', btnText: "Войдите в личный кабинет" }}
                description={"Наше приложение - ваш верный помощник в организации любых мероприятий, от незабываемых свадеб и уютных дней рождения до масштабных корпоративов и тематических вечеринок. Мы поможем вам спланировать каждую деталь, чтобы ваш праздник прошел гладко и радостно."}
            />
            <ContentBlock
                title="Создание мероприятия"
                description="Легко планируйте и создавайте мероприятия с помощью интуитивно понятного интерфейса. Укажите дату, время и тип мероприятия, чтобы начать подготовку."
                src={ImBlock2}
                buttonSettings={{ btnText: 'Начать' }}
                onBtnClick={showModal}
                isTextFirst={true}
                variant="white-bg-left-text-right-image"
            />
            <ContentBlock
                title="Добавление гостей"
                description="Управляйте своими гостями, добавляя их в приложение и распределяя по категориям и столам. Учитывайте предпочтения и определяйте индивидуальное меню для каждого гостя."
                src={ImBlock3}
                isTextFirst={true}
                variant="white-bg-left-image-right-text"
            />
            <ContentBlock
                title="Добавление подрядчиков"
                description="Найдите и добавьте подрядчиков, таких как рестораны, ведущие, аниматоры и другие специалисты. Удобно отслеживайте их контактные данные и статусы сотрудничества."
                src={ImBlock4}
                buttonSettings={{ btnText: 'Начать' }}
                onBtnClick={showModal}
                isTextFirst={true}
                variant="blue-bg-left-text-right-image"
            />
            <ContentBlock
                title="Управление бюджетом"
                description="Контролируйте расходы на ваше мероприятие с помощью встроенного инструмента управления бюджетом. Настраивайте лимиты и отслеживайте, чтобы оставаться в рамках запланированных затрат."
                src={ImBlock5}
                isTextFirst={true}
                variant="white-bg-left-image-right-text"
            />
            <ContentBlock
                title="Расписание"
                description="Создавайте и отслеживайте задачи для каждого этапа подготовки. Установите сроки и напоминания, чтобы ничего не упустить в процессе организации вашего праздника."
                src={ImBlock6}
                buttonSettings={{ btnText: 'Начать' }}
                onBtnClick={showModal}
                isTextFirst={true}
                variant="white-bg-left-text-right-image"
            />
            <AuthorizationModal
                visible={isModalVisible}
                onCancel={handleCancel}
                setIsAuth={setIsAuth}
            />
        </>
    );
};