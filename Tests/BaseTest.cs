﻿global using NUnit.Framework;
global using Microsoft.Playwright;

namespace Saucedemo.Tests;

public abstract class BaseTest
{
    protected IPlaywright Playwright;
    protected IBrowser Browser;
    protected IPage Page;
    protected IBrowserContext Context;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Channel = "chrome",
            Args = new[] { "--start-maximized" }
        });
    }

    [SetUp]
    public async Task Setup()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Channel = "chrome",
            Args = new[] { "--start-maximized" }
        });

        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Height = 780,
                Width = 1400
            },
            IgnoreHTTPSErrors = true,
            JavaScriptEnabled = true,
            HasTouch = false
        });

        //Page = await Context.NewPageAsync();
        //await Page.SetDefaultNavigationTimeoutAsync(30000); // Increased timeout for stability
        //await Page.SetDefaultTimeoutAsync(10000);
    }

    [TearDown]
    public async Task Teardown()
    {
        Thread.Sleep(3000);
        try
        {
            if (Page != null)
                await Page.CloseAsync();
            if (Context != null)
                await Context.CloseAsync();
            if (Browser != null)
                await Browser.CloseAsync();
        }
        finally
        {
            Playwright?.Dispose();
        }
    }
}