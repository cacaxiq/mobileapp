﻿using System;
using System.Reactive.Concurrency;
using MvvmCross.Core.Navigation;
using Toggl.Foundation.Analytics;
using Toggl.Foundation.Login;
using Toggl.Foundation.MvvmCross.Services;
using Toggl.Foundation.Services;
using Toggl.Foundation.Shortcuts;
using Toggl.Foundation.Suggestions;
using Toggl.Multivac;
using Toggl.PrimeRadiant;
using Toggl.PrimeRadiant.Settings;
using Toggl.Ultrawave;
using Toggl.Ultrawave.Network;

namespace Toggl.Foundation.MvvmCross
{
    public struct MvvmCrossFoundation
    {
        public Version Version { get; }
        public UserAgent UserAgent { get; }
        public IApiFactory ApiFactory { get; }
        public ITogglDatabase Database { get; }
        public ITimeService TimeService { get; }
        public IScheduler Scheduler { get; }
        public IMailService MailService { get; }
        public IGoogleService GoogleService { get; }
        public ApiEnvironment ApiEnvironment { get; }
        public ILicenseProvider LicenseProvider { get; }
        public IAnalyticsService AnalyticsService { get; }
        public IBackgroundService BackgroundService { get; }
        public IPlatformConstants PlatformConstants { get; }
        public IApplicationShortcutCreator ShortcutCreator { get; }
        public ISuggestionProviderContainer SuggestionProviderContainer { get; }

        public IDialogService DialogService { get; }
        public IBrowserService BrowserService { get; }
        public IKeyValueStorage KeyValueStorage { get; }
        public IUserPreferences UserPreferences { get; }
        public IOnboardingStorage OnboardingStorage { get; }
        public IMvxNavigationService NavigationService { get; }
        public IPasswordManagerService PasswordManagerService { get; }
        public IApiErrorHandlingService ApiErrorHandlingService { get; }
        public IAccessRestrictionStorage AccessRestrictionStorage { get; }

        private MvvmCrossFoundation(Builder builder)
        {
            builder.EnsureValidity();

            DialogService = builder.DialogService;
            BrowserService = builder.BrowserService;
            KeyValueStorage = builder.KeyValueStorage;
            UserPreferences = builder.UserPreferences;
            OnboardingStorage = builder.OnboardingStorage;
            NavigationService = builder.NavigationService;
            PasswordManagerService = builder.PasswordManagerService;
            ApiErrorHandlingService = builder.ApiErrorHandlingService;
            AccessRestrictionStorage = builder.AccessRestrictionStorage;

            Version = builder.Foundation.Version;
            Database = builder.Foundation.Database;
            UserAgent = builder.Foundation.UserAgent;
            Scheduler = builder.Foundation.Scheduler;
            ApiFactory = builder.Foundation.ApiFactory;
            TimeService = builder.Foundation.TimeService;
            MailService = builder.Foundation.MailService;
            GoogleService = builder.Foundation.GoogleService;
            ApiEnvironment = builder.Foundation.ApiEnvironment;
            LicenseProvider = builder.Foundation.LicenseProvider;
            ShortcutCreator = builder.Foundation.ShortcutCreator;
            AnalyticsService = builder.Foundation.AnalyticsService;
            PlatformConstants = builder.Foundation.PlatformConstants;
            BackgroundService = builder.Foundation.BackgroundService;
            SuggestionProviderContainer = builder.Foundation.SuggestionProviderContainer;
        }

        public class Builder
        {
            internal TogglFoundation Foundation { get; }
            public IDialogService DialogService { get; private set; }
            public IBrowserService BrowserService { get; private set; }
            public IKeyValueStorage KeyValueStorage { get; private set; }
            public IUserPreferences UserPreferences { get; private set; }
            public IOnboardingStorage OnboardingStorage { get; private set; }
            public IMvxNavigationService NavigationService { get; private set; }
            public IPasswordManagerService PasswordManagerService { get; private set; }
            public IApiErrorHandlingService ApiErrorHandlingService { get; private set; }
            public IAccessRestrictionStorage AccessRestrictionStorage { get; private set; }

            public Builder(TogglFoundation foundation)
            {
                Ensure.Argument.IsNotNull(foundation, nameof(foundation));

                Foundation = foundation;
            }

            public Builder WithDialogService(IDialogService dialogService)
            {
                DialogService = dialogService;
                return this;
            }

            public Builder WithBrowserService(IBrowserService browserService)
            {
                BrowserService = browserService;
                return this;
            }

            public Builder WithKeyValueStorage(IKeyValueStorage keyValueStorage)
            {
                KeyValueStorage = keyValueStorage;
                return this;
            }

            public Builder WithAccessRestrictionStorage(IAccessRestrictionStorage accessRestrictionStorage)
            {
                AccessRestrictionStorage = accessRestrictionStorage;
                return this;
            }

            public Builder WithUserPreferences(IUserPreferences userPreferences)
            {
                UserPreferences = userPreferences;
                return this;
            }

            public Builder WithOnboardingStorage(IOnboardingStorage onboardingStorage)
            {
                OnboardingStorage = onboardingStorage;
                return this;
            }

            public Builder WithNavigationService(IMvxNavigationService navigationService)
            {
                NavigationService = navigationService;
                return this;
            }

            public Builder WithPasswordManagerService(IPasswordManagerService passwordManagerService)
            {
                PasswordManagerService = passwordManagerService;
                return this;
            }

            public Builder WithApiErrorHandlingService(IApiErrorHandlingService apiErrorHandlingService)
            {
                ApiErrorHandlingService = apiErrorHandlingService;
                return this;
            }

            public Builder WithDialogService<TDialogService>()
                where TDialogService : IDialogService, new()
                => WithDialogService(new TDialogService());

            public Builder WithBrowserService<TBrowserService>()
                where TBrowserService : IBrowserService, new()
                => WithBrowserService(new TBrowserService());

            public Builder WithKeyValueStorage<TKeyValueStorage>()
                where TKeyValueStorage : IKeyValueStorage, new()
                => WithKeyValueStorage(new TKeyValueStorage());

            public Builder WithAccessRestrictionStorage<TAccessRestrictionStorage>()
                where TAccessRestrictionStorage : IAccessRestrictionStorage, new()
                => WithAccessRestrictionStorage(new TAccessRestrictionStorage());

            public Builder WithUserPreferences<TUserPreferences>()
                where TUserPreferences : IUserPreferences, new()
                => WithUserPreferences(new TUserPreferences());

            public Builder WithOnboardingStorage<TOnboardingStorage>()
                where TOnboardingStorage : IOnboardingStorage, new()
                => WithOnboardingStorage(new TOnboardingStorage());

            public Builder WithNavigationService<TNavigationService>()
                where TNavigationService : IMvxNavigationService, new()
                => WithNavigationService(new TNavigationService());

            public Builder WithPasswordManagerService<TPasswordManagerService>()
                where TPasswordManagerService : IPasswordManagerService, new()
                => WithPasswordManagerService(new TPasswordManagerService());

            public Builder WithApiErrorHandlingService<TApiErrorHandlingService>()
                where TApiErrorHandlingService : IApiErrorHandlingService, new()
                => WithApiErrorHandlingService(new TApiErrorHandlingService());

            public MvvmCrossFoundation Build()
                => new MvvmCrossFoundation(this);

            public void EnsureValidity()
            {
                Ensure.Argument.IsNotNull(DialogService, nameof(DialogService));
                Ensure.Argument.IsNotNull(BrowserService, nameof(BrowserService));
                Ensure.Argument.IsNotNull(KeyValueStorage, nameof(KeyValueStorage));
                Ensure.Argument.IsNotNull(UserPreferences, nameof(UserPreferences));
                Ensure.Argument.IsNotNull(OnboardingStorage, nameof(OnboardingStorage));
                Ensure.Argument.IsNotNull(NavigationService, nameof(NavigationService));
                Ensure.Argument.IsNotNull(ApiErrorHandlingService, nameof(ApiErrorHandlingService));
                Ensure.Argument.IsNotNull(AccessRestrictionStorage, nameof(AccessRestrictionStorage));
            }
        }
    }
}
