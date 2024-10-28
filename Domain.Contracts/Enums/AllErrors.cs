namespace Exemple.Identity.Domain.Contracts.Enums
{
    /// <summary>
    /// AllErrors
    /// </summary>
    public enum AllErrors
    {
        #region Global 0x00**
        /// <summary>
        /// NoError
        /// </summary>
        NoError = 0x0000,

        /// <summary>
        /// General
        /// </summary>
        General = 0x0001,

        /// <summary>
        /// DatabaseError
        /// </summary>
        DatabaseError = 0x0003,

        /// <summary>
        /// ValidationErrors
        /// </summary>
        ValidationErrors = 0x0004,
        #endregion

        #region Common 0x01**
        #endregion

        #region Category 0x02**
        /// <summary>
        /// CategoryNotExists
        /// </summary>
        CategoryNotExists = 0x0200,

        /// <summary>
        /// CategoryNotExists
        /// </summary>
        ParentCategoryNotExists = 0x0201,

        /// <summary>
        /// DuplicateCategory
        /// </summary>
        DuplicateCategory = 0x0202,
        #endregion

        #region Attribute 0x03**
        /// <summary>
        /// AttributeNotExists
        /// </summary>
        AttributeNotExists = 0x0300,

        /// <summary>
        /// DuplicateAttribute
        /// </summary>
        DuplicateAttribute = 0x0301,
        #endregion

        #region User 0x04**
        /// <summary>
        /// UserNotExists
        /// </summary>
        UserNotExists = 0x0400,

        /// <summary>
        /// UserNotExistsOrPasswordIncorrect
        /// </summary>
        UserNotExistsOrPasswordIncorrect = 0x0401,

        /// <summary>
        /// AccessDenied
        /// </summary>
        AccessDenied = 0x0402,

        /// <summary>
        /// TokenIsEmpty
        /// </summary>
        TokenIsEmpty = 0x0403,

        /// <summary>
        /// RefreshTokenIsEmpty
        /// </summary>
        RefreshTokenIsEmpty = 0x0404,

        /// <summary>
        /// InvalidReferalLink
        /// </summary>
        InvalidReferalLink = 0x0405,

        /// <summary>
        /// ReferalOwnerNotExists
        /// </summary>
        ReferalOwnerNotExists = 0x0406,

        /// <summary>
        /// DuplicateUser
        /// </summary>
        DuplicateUser = 0x0407,

        /// <summary>
        /// TooManyUsers
        /// </summary>
        TooManyUsers = 0x0408,

        /// <summary>
        /// InvalidRoleGroup
        /// </summary>
        InvalidRoleGroup = 0x0409,

        /// <summary>
        /// InvalidUserId
        /// </summary>
        InvalidUserId = 0x040a,

        /// <summary>
        /// RunOutOfViews
        /// </summary>
        RunOutOfViews = 0x040b,

        /// <summary>
        /// VerificationCodeNotExists
        /// </summary>
        VerificationCodeNotExists = 0x040c,

        /// <summary>
        /// VerificationCodeNotConfirmed
        /// </summary>
        VerificationCodeNotConfirmed = 0x040d,

        /// <summary>
        /// VerificationCodeIncorrect
        /// </summary>
        VerificationCodeIncorrect = 0x040e,

        /// <summary>
        /// VerificationCodeOutdated
        /// </summary>
        VerificationCodeOutdated = 0x040f,

        /// <summary>
        /// UserNotVerified
        /// </summary>
        UserNotVerified = 0x0410,
        #endregion

        #region Activity 0x05**
        #endregion

        #region File 0x06**
        /// <summary>
        /// FileNotExists
        /// </summary>
        InvalidFileId = 0x0600,

        /// <summary>
        /// FileNotExists
        /// </summary>
        FileNotExists = 0x0601,

        /// <summary>
        /// FailedToCreateFile
        /// </summary>
        FailedToCreateFile = 0x0602,

        /// <summary>
        /// FailedToUpdateFile
        /// </summary>
        FailedToUpdateFile = 0x0603,
        #endregion

        #region Product 0x07**
        /// <summary>
        /// ForbiddenTariffProduct
        /// </summary>
        ForbiddenTariffProduct = 0x0700,
        #endregion

        #region Order 0x08**
        /// <summary>
        /// InvalidOrderId
        /// </summary>
        InvalidOrderId = 0x0800,

        /// <summary>
        /// OrderNotExists
        /// </summary>
        OrderNotExists = 0x0801,

        /// <summary>
        /// NoOpenOrder
        /// </summary>
        NoOpenedOrder = 0x0802,

        /// <summary>
        /// EmptyOrder
        /// </summary>
        EmptyOrder = 0x0803,

        /// <summary>
        /// NothingToPay
        /// </summary>
        NothingToPay = 0x0804,

        /// <summary>
        /// ForbiddenStatus
        /// </summary>
        ForbiddenStatus = 0x0805,

        /// <summary>
        /// InvalidPaymentToken
        /// </summary>
        InvalidPaymentToken = 0x0807,
        #endregion

        #region Schedule 0x09**
        /// <summary>
        /// ServiceNotExists
        /// </summary>
        ServiceNotExists = 0x0900,

        /// <summary>
        /// ServiceNotSaved
        /// </summary>
        ServiceNotSaved = 0x0901,
        #endregion

        #region Reward 0x0a**
        /// <summary>
        /// OnlyOneActiveRequestAllowed
        /// </summary>
        OnlyOneActiveRequestAllowed = 0x0a00,

        /// <summary>
        /// RewardRequestNotExists
        /// </summary>
        RequestNotExists = 0x0a01,

        /// <summary>
        /// RewardInsufficientFunds
        /// </summary>
        RewardInsufficientFunds = 0x0a02,

        /// <summary>
        /// NoRewards
        /// </summary>
        NoRewards = 0x0a03,
        #endregion

        #region Model validation 0x0b**
        #endregion

        #region Payment 0x0c**
        /// <summary>
        /// PaymentNotExists
        /// </summary>
        PaymentNotExists = 0x0c00,

        /// <summary>
        /// PaymentRequestNotExists
        /// </summary>
        PaymentRequestNotExists = 0x0c01,
        #endregion

        #region Investment 0x0d**
        /// <summary>
        /// InvestmentNotExists
        /// </summary>
        InvestmentNotExists = 0x0d00,

        /// <summary>
        /// InvestmentAmountTooSmall
        /// </summary>
        InvestmentAmountTooSmall = 0x0d01,

        /// <summary>
        /// InvestmentRateTooSmall
        /// </summary>
        InvestmentRateTooSmall = 0x0d02,

        /// <summary>
        /// InvestmentInsufficientFunds
        /// </summary>
        InvestmentInsufficientFunds = 0x0d03,
        #endregion

        #region Promo 0x0e**
        /// <summary>
        /// PromoProgramNotExists
        /// </summary>
        PromoProgramNotExists = 0x0e00,

        /// <summary>
        /// PromoCodeNotExists
        /// </summary>
        PromoCodeNotExists
        #endregion
    }
}
