﻿using System.Web.Optimization;

namespace ReadingClub.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerysignalR").Include(
                      "~/Scripts/jquery.signalR-2.1.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
                      "~/Scripts/gridmvc.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/getTimezoneOffset").Include(
                      "~/Scripts/js/getTimezoneOffset.js"));

            bundles.Add(new ScriptBundle("~/bundles/timeParser").Include(
                      "~/Scripts/js/timeParser.js"));

            bundles.Add(new ScriptBundle("~/bundles/participantsHub").Include(
                      "~/Scripts/js/participantsHub.js"));

            bundles.Add(new ScriptBundle("~/bundles/chatHub").Include(
                      "~/Scripts/js/chatHub.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Gridmvc").Include(
                      "~/Content/Gridmvc.css"));
        }
    }
}